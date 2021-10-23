using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public PlayerStats playerStats;
    public ItemData item;
    public TMP_Text text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void Initialize(ItemData item)
    {
        this.item = item;
        text.text = item.cost.ToString();
    }
    private void Update()
    {
        if (item == null) { Destroy(gameObject); }
        if (item.cost > playerStats.stanleyWeight)
        {
            _button.interactable = false;
            text.color = Color.red;
        }
        else
        {
            _button.interactable = true;
            text.color = Color.white;
        }
    }
}
