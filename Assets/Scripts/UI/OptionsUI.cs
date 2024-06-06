using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindKeyTransfrom;
    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicsButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Move_Up));
        moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Move_Down));
        moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Move_Left));
        moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Move_Right));
        interactButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Interact));
        interactAlternateButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.InteractAlternate));
        pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Bingding.Pause));
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);
        moveUpText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Move_Right);
        interactText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBingdingText(GameInput.Bingding.Pause);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransfrom.gameObject.SetActive(false);
    }
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransfrom.gameObject.SetActive(true);
    }

    private void RebindBinding(GameInput.Bingding bingding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(bingding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
