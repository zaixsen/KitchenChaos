using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public const string PLAYER_REFRES_BINDINGS = "PlayerRefresBindings";

    public event EventHandler OnInteractAction;   //交互绑定 E
    public event EventHandler OnInteractAlternateAction;   //交互绑定 F
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    public enum Bingding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }

    private PlayerInputActions inputActions;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActions();

        //设置存储绑定的按键
        //if (PlayerPrefs.HasKey(PLAYER_REFRES_BINDINGS))
        //{
        //    inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_REFRES_BINDINGS));
        //}

        inputActions.Player.Enable();          //手动激活
        inputActions.Player.Interact.performed += Interact_performed;   //按键 E 触发事件 
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed; ;   //按键 F 触发事件 
        inputActions.Player.Pause.performed += Pause_performed;

    }
    private void OnDestroy()
    {
        inputActions.Player.Interact.performed -= Interact_performed;   //按键 E 触发事件 
        inputActions.Player.InteractAlternate.performed -= InteractAlternate_performed; ;   //按键 F 触发事件 
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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

    public string GetBingdingText(Bingding bingding)
    {
        //这里的索引是input界面的索引
        switch (bingding)
        {
            default:
            case Bingding.Move_Up:
                return inputActions.Player.Move.bindings[1].ToDisplayString();
            case Bingding.Move_Down:
                return inputActions.Player.Move.bindings[2].ToDisplayString();
            case Bingding.Move_Left:
                return inputActions.Player.Move.bindings[3].ToDisplayString();
            case Bingding.Move_Right:
                return inputActions.Player.Move.bindings[4].ToDisplayString();
            case Bingding.Interact:
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Bingding.InteractAlternate:
                return inputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Bingding.Pause:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    /// <summary>
    /// 切换按键
    /// </summary>
    /// <param name="bingding"></param>
    /// <param name=""></param>
    public void RebindBinding(Bingding bingding, Action onActionRrbound)
    {

        //PlayerInputActions inputActions  此类为input系统生成的 这里切换相应的按键
        inputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (bingding)
        {
            default:
            case Bingding.Move_Up:
                inputAction = inputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Bingding.Move_Down:
                inputAction = inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Bingding.Move_Left:
                inputAction = inputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Bingding.Move_Right:
                inputAction = inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Bingding.Interact:
                inputAction = inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Bingding.InteractAlternate:
                inputAction = inputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Bingding.Pause:
                inputAction = inputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)  //这个需要引入命名空间  using UnityEngine.InputSystem;
            .OnComplete(callback =>
        {
            callback.Dispose();
            inputActions.Player.Enable();
            onActionRrbound?.Invoke();
            //将按键信息存储json  进行按键换绑记录 以便下次打开不换按键
            // inputActions.SaveBindingOverridesAsJson();
            // PlayerPrefs.SetString(PLAYER_REFRES_BINDINGS, inputActions.SaveBindingOverridesAsJson());
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
            // PlayerPrefs.Save();
        })
            .Start();
    }


}
