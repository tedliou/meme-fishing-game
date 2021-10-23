using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Canvas fishingInformationCanvas;
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