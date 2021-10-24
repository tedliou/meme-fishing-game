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
    private void Awake()
    {
        playerStats.selectedBait = baits[0];
        playerStats.selectedRod = fishingRods[0];
        playerStats.stanleyPower = stanleyPower[0];

        buttons[0].GetComponent<ButtonController>().Initialize(fishingRods);
        buttons[0].onClick.AddListener(() => UpgradeRod());

        buttons[1].GetComponent<ButtonController>().Initialize(baits);
        buttons[1].onClick.AddListener(() => UpgradeBait());

        buttons[2].GetComponent<ButtonController>().Initialize(baitLifetime);
        buttons[2].onClick.AddListener(() => UpgradeBaitLifetime());

        buttons[3].GetComponent<ButtonController>().Initialize(stanleyPower);
        buttons[3].onClick.AddListener(() => UpgradeStanleyPower());
    }
    public void UpgradeRod()
    {
        ItemData item = fishingRods[Mathf.Min(2, playerStats.fishingRodLevel)];
        fishingPole.SwitchRod(item);
        playerStats.stanleyWeight -= item.cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-item.cost).ToString(), Color.red);
        playerStats.fishingRodLevel++;
    }
    public void UpgradeBait()
    {
        playerStats.selectedBait = baits[playerStats.baitLevel];
        playerStats.stanleyWeight -= baits[playerStats.baitLevel].cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-baits[playerStats.baitLevel].cost).ToString(), Color.red);
        fishingPole.SwitchBait();
        playerStats.baitLevel++;
    }
    public void UpgradeStanleyPower()
    {
        playerStats.stanleyPower = stanleyPower[playerStats.powerLevel];
        playerStats.stanleyWeight -= stanleyPower[playerStats.baitLevel].cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-stanleyPower[playerStats.baitLevel].cost).ToString(), Color.red);
        playerStats.powerLevel++;
    }
    public void UpgradeBaitLifetime()
    {
        playerStats.lifeTimeLevel++;
        playerStats.stanleyWeight -= baitLifetime[playerStats.lifeTimeLevel].cost;
        GameManager.instance.SpawnPopup(Vector3.zero, (-baitLifetime[playerStats.lifeTimeLevel].cost).ToString(), Color.red);
    }
}
