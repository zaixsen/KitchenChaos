using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyinteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownStartActive())
        {
            Hide();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Interact);
        keyinteractAlternateText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Pause);
    }
}
