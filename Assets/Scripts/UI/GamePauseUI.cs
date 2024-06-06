using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        ResumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += GamePaused_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnPaused += OnGameUnPaused_OnGameUnPaused;
        Hide();
    }

    private void OnGameUnPaused_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GamePaused_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
