using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public Sprite[] fishingRods;
    public PlayerStats playerStats;
    public Button[] buttons;

    public SpriteRenderer fishingPoleSprite;
    private void Start()
    {
        playerStats.fishingRodLevel = 0;
        fishingPoleSprite.sprite = fishingRods[0];


    }
    public void UpgradeRod()
    {
        playerStats.fishingRodLevel++;
        fishingPoleSprite.sprite = fishingRods[Mathf.Min(2, playerStats.fishingRodLevel)];
    }

    public void UpgradeStanleyPower()
    {

    }
    public void UpgradeBaitLifetime()
    {

    }
}
