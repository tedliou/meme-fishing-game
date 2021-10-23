using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float reelPower = 0.2f;
    public float lineLength = 3f;
    public float tossPower = 10;
}
