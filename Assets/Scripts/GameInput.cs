using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public const string PLAYER_REFRES_BINDINGS = "PlayerRefresBindings";

    public event EventHandler OnInteractAction;   //������ E
    public event EventHandler OnInteractAlternateAction;   //������ F
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

        //���ô洢�󶨵İ���
        //if (PlayerPrefs.HasKey(PLAYER_REFRES_BINDINGS))
        //{
        //    inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_REFRES_BINDINGS));
        //}

        inputActions.Player.Enable();          //�ֶ�����
        inputActions.Player.Interact.performed += Interact_performed;   //���� E �����¼� 
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed; ;   //���� F �����¼� 
        inputActions.Player.Pause.performed += Pause_performed;

    }
    private void OnDestroy()
    {
        inputActions.Player.Interact.performed -= Interact_performed;   //���� E �����¼� 
        inputActions.Player.InteractAlternate.performed -= InteractAlternate_performed; ;   //���� F �����¼� 
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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

    public string GetBingdingText(Bingding bingding)
    {
        //�����������input���������
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
    /// �л�����
    /// </summary>
    /// <param name="bingding"></param>
    /// <param name=""></param>
    public void RebindBinding(Bingding bingding, Action onActionRrbound)
    {

        //PlayerInputActions inputActions  ����Ϊinputϵͳ���ɵ� �����л���Ӧ�İ���
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

        inputAction.PerformInteractiveRebinding(bindingIndex)  //�����Ҫ���������ռ�  using UnityEngine.InputSystem;
            .OnComplete(callback =>
        {
            callback.Dispose();
            inputActions.Player.Enable();
            onActionRrbound?.Invoke();
            //��������Ϣ�洢json  ���а��������¼ �Ա��´δ򿪲�������
            // inputActions.SaveBindingOverridesAsJson();
            // PlayerPrefs.SetString(PLAYER_REFRES_BINDINGS, inputActions.SaveBindingOverridesAsJson());
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
            // PlayerPrefs.Save();
        })
            .Start();
    }


}
