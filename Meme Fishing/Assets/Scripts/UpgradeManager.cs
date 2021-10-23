using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public ItemData[] fishingRods;
    public PlayerStats playerStats;
    public Button[] buttons;

    public FishingPole fishingPole;
    private void Start()
    {
        buttons[0].GetComponent<ButtonController>().Initialize(fishingRods[1]);
        buttons[0].onClick.AddListener(() => UpgradeRod());
    }
    public void UpgradeRod()
    {
        playerStats.fishingRodLevel++;
        fishingPole.SwitchRod(fishingRods[Mathf.Min(2, playerStats.fishingRodLevel)]);
        buttons[0].GetComponent<ButtonController>().Initialize(fishingRods[Mathf.Min(2, playerStats.fishingRodLevel)]);
    }

    public void UpgradeStanleyPower()
    {

    }
    public void UpgradeBaitLifetime()
    {

    }
}
