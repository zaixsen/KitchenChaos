using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    //用于视觉选中交互
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;    //物品位置

    private bool Iswalking;            //移动动画
    private float rotateSpeed = 10f;   //旋转方向
    private float playerRadius = .7f;  //玩家半径
    private float playerHeight = 2f;   //玩家高度
    private Vector3 lastInteractDir;   //最后的交互方向

    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;    //玩家持有当前拿取的物品

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //按键E触发
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
    /// 交互
    /// </summary>
    private void HandleInteractions()
    {
        //获取输入轴向
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //移动方向
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        //交互距离
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
            else  //检测的的物品没有 BaseCounter 脚本
            {
                SetSelectedCounter(null);
            }
        }
        else  //没检测到
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
        //获取输入轴向
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //移动方向
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        Iswalking = moveDir != Vector3.zero;                //移动动画

        float moveDistance = moveSpeed * Time.deltaTime;    //移动距离
        //身边是否有物体                       最低点                            最高点                               半径       方向      距离
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //解决贴墙不能对角线移动的问题
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
            //前方为将要移动的地方 面向
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
    public bool IsWalking()
    {
        return Iswalking;
    }

    //设置厨房物品
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    //获取厨房物品
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    //清除厨房物品
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    //是否含有厨房物品
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
}
