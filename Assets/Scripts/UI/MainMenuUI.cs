using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button play_Btn;
    [SerializeField] private Button quit_Btn;

    private void Awake()
    {
        play_Btn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        quit_Btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1f;
    }
}
