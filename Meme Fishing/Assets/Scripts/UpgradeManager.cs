using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public ItemData[] fishingRods;
    public ItemData[] baits;
    public ItemData[] baitLifetime;
    public ItemData[] stanleyPower;

    public PlayerStats playerStats;
    public Button[] buttons;

    public FishingPole fishingPole;
    private void Start()
    {
        buttons[0].GetComponent<ButtonController>().Initialize(fishingRods);
        buttons[1].GetComponent<ButtonController>().Initialize(baits);
        //buttons[2].GetComponent<ButtonController>().Initialize(baitLifetime);
        //buttons[3].GetComponent<ButtonController>().Initialize(stanleyPower);
    }
    public void UpgradeRod()
    {
        playerStats.fishingRodLevel += 1;
        ItemData item = fishingRods[Mathf.Min(2, playerStats.fishingRodLevel)];
        fishingPole.SwitchRod(item);
        playerStats.stanleyWeight -= item.cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-item.cost).ToString(), Color.red);
    }
    public void UpgradeBait()
    {
        playerStats.selectedBait = baits[buttons[1].GetComponent<ButtonController>().level];
        playerStats.stanleyWeight -= baits[buttons[1].GetComponent<ButtonController>().level].cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-baits[buttons[1].GetComponent<ButtonController>().level].cost).ToString(), Color.red);
    }
    public void UpgradeStanleyPower()
    {

    }
    public void UpgradeBaitLifetime()
    {

    }
}
