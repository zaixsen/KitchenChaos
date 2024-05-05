using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    //�����Ӿ�ѡ�н���
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;    //��Ʒλ��

    private bool Iswalking;            //�ƶ�����
    private float rotateSpeed = 10f;   //��ת����
    private float playerRadius = .7f;  //��Ұ뾶
    private float playerHeight = 2f;   //��Ҹ߶�
    private Vector3 lastInteractDir;   //���Ľ�������

    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;    //��ҳ��е�ǰ��ȡ����Ʒ

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //����E����
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;      
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void HandleInteractions()
    {
        //��ȡ��������
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //�ƶ�����
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        //��������
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else  //���ĵ���Ʒû�� BaseCounter �ű�
            {
                SetSelectedCounter(null);
            }
        }
        else  //û��⵽
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = selectedCounter
        });
    }

    private void HandleMovement()
    {
        //��ȡ��������
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //�ƶ�����
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        Iswalking = moveDir != Vector3.zero;                //�ƶ�����

        float moveDistance = moveSpeed * Time.deltaTime;    //�ƶ�����
        //����Ƿ�������                       ��͵�                            ��ߵ�                               �뾶       ����      ����
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //�����ǽ���ܶԽ����ƶ�������
        if (!canMove)
        {
            // connot move towards moveDir
            // Attemp only X movement 
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDirX.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {// Cannot move int any direction}
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        if (moveDir != Vector3.zero)
        {
            //ǰ��Ϊ��Ҫ�ƶ��ĵط� ����
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
    public bool IsWalking()
    {
        return Iswalking;
    }

    //���ó�����Ʒ
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    //��ȡ������Ʒ
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    //���������Ʒ
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    //�Ƿ��г�����Ʒ
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
}
