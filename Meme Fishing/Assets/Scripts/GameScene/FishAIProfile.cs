using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish")]
public class FishAIProfile : ScriptableObject
{
    public Sprite[] sprites;
    public AudioClip sfx;
    public string content;
    public int weight;
}
