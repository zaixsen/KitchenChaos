using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;   //������ E
    public event EventHandler OnInteractAlternateAction;   //������ F

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();          //�ֶ�����
        inputActions.Player.Interact.performed += Interact_performed;   //���� E �����¼� 
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed; ;   //���� F �����¼� 
    }

    //��F����
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    //�����������¼�
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);  //�¼���
    }

    //��ȡ����
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        //��һ������
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
