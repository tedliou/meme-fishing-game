using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bait")]
public class BaitData : ScriptableObject
{
    public int cost;
    public GameObject baitPrefab;
    public float mass;
    public float gravity;
}
