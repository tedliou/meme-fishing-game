using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Stanley")]
    public int stanleyWeight = 0;
    public float reelPower = 1f;
    public float stanleyPower = 10;

    [Header("Fishing Pole")]
    public int fishingRodLevel = 0;
    public float lineLength = 3f;

    [Header("Bait")]
    public ItemData selectedBait;
    public float additionalLifetime;

    public float LifeTime { get => selectedBait.lifeTime + additionalLifetime; }
}
