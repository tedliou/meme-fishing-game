using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Stanley")]
    public int stanleyWeight = 0;
    public float stanleyPower = 0.5f;

    [Header("Fishing Pole")]
    public int fishingRodLevel = 0;
    public float lineLength = 3f;

    [Header("Bait")]
    public ItemData selectedBait;
    public int baitLevel = 0;
    public int lifeTimeLevel = 0;

    public float LifeTime { get => selectedBait.lifeTime + lifeTimeLevel * 0.5f; }
}
