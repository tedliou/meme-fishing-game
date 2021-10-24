using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public int spawnCount;
    public GameObject fish;

    private void Start () {
        Debug.Log("Spawn " + spawnCount);
        for (int i = 0; i < spawnCount; i++)
        {
            var pos = new Vector3(Random.Range(2, 50f), Random.Range(-8f, -27.5f), 0);
            var obj = Instantiate(fish);
            obj.transform.position = pos;
        }
    }
}
