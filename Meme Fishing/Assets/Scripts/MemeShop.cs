using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MemeShop : MonoBehaviour
{
    public BaitData[] baits;
    public Button[] buttons;
    public FishingPole fishingPole;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < baits.Length; i++)
        {
            buttons[i].onClick.AddListener(() => SwitchBait(baits[i]));
        }
    }
    public void SwitchBait(BaitData baitData)
    {

    }
}
