using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Canvas fishingInformationCanvas;
    public Image baitTimerFill;
    public static GameManager instance;

    public State state;
    public PlayerStats playerStats;

    public void Awake()
    {
        instance = this;

        playerStats.fishingRodLevel = 0;
        playerStats.stanleyPower = 1;
        playerStats.additionalLifetime = 0;
        playerStats.stanleyWeight = 1000;
    }
    public void UpdateBaitTimer(float time, float maxTime)
    {
        baitTimerFill.rectTransform.sizeDelta = new Vector2(Mathf.Min(maxTime * 50, 1500), 30);
        baitTimerFill.fillAmount = time / maxTime;
    }
    public void AddWeight(int weight)
    {
        playerStats.stanleyWeight += weight;
    }
}
public enum State
{
    Throwing,
    Fishing,
    Standby,
}