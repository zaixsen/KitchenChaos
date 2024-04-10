using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;   //交互绑定 E
    public event EventHandler OnInteractAlternateAction;   //交互绑定 F

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();          //手动激活
        inputActions.Player.Interact.performed += Interact_performed;   //按键 E 触发事件 
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed; ;   //按键 F 触发事件 
    }

    //按F触发
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    //交互侦听的事件
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);  //事件绑定
    }

    //获取轴向
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        //归一化输入
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
