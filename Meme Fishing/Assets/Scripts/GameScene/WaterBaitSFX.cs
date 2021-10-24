using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBaitSFX : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Bait")) GetComponent<AudioSource>().Play();
    }
}
