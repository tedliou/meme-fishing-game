using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Stanley")]
    public float reelPower = 1f;
    public float stanleyPower = 10;

    [Header("Fishing Pole")]
    public float lineLength = 3f;

    [Header("Bait")]
    public float mass = 0.1f;
    public float gravity = 1f;
}
