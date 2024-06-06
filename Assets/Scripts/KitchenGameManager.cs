using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> 
/// 游戏管理
/// 兼顾 游戏的开始 暂停 和结束
/// </summary>
public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;


    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    //private float waitingToStartTimer = 1f;
    private float countdownoStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePaused = false;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction; ;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
        }
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownoStartTimer -= Time.deltaTime;
                if (countdownoStartTimer < 0)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        if (state == State.GameOver)
        {
            return false;
        }
        return true;
    }

    public bool IsCountdownStartActive()
    {
        return state == State.CountdownToStart;
    }
    public float GetCountdownToStartTimer()
    {
        return countdownoStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0;
        }
        else
        {
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1;
        }


    }
}
