using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPole : MonoBehaviour
{
    public GameObject fishingPole;
    void Start()
    {
        fishingPole = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
