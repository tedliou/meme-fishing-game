using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public int cost;
    public GameObject itemPrefab;
    public float lifeTime;

    public float mass;
    public float gravity;
}
