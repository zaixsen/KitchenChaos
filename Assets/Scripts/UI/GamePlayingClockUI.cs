using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{

    [SerializeField] private Image timerImager;

    private void Update()
    {
        timerImager.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }

}
