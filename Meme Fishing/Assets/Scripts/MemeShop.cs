using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MemeShop : MonoBehaviour
{
    public ItemData[] baits;
    public PlayerStats playerStats;
    public Button[] buttons;
    public FishingPole fishingPole;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
