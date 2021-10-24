using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    public TMP_Text text;
    public void Initialize(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;

        Vector3 newPos = Random.insideUnitCircle + new Vector2(0, 2f);
        transform.DOMove(newPos, 1f);
        transform.DOScale(2f, 1f);
        Destroy(gameObject, 1.2f);
    }
}
