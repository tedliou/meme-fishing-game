using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemeShop : MonoBehaviour
{
    public ItemData[] baits;
    public PlayerStats playerStats;
    public Button[] buttons;
    public FishingPole fishingPole;

    private void Awake()
    {
        playerStats.selectedBait = baits[0];

        for (int i = 0; i < baits.Length; i++)
        {
            buttons[i].GetComponent<ButtonController>().Initialize(baits[i]);
            ItemData data = baits[i];
            buttons[i].GetComponent<Button>().onClick.AddListener(() => fishingPole.SwitchBait(data));
            buttons[i].GetComponent<Button>().onClick.AddListener(() => playerStats.selectedBait = data);
        }
    }
}
