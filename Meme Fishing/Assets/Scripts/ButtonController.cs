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
    public ItemData[] items;
    public TMP_Text text;
    private Button _button;
    public int level = 1;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => level++);
    }
    public void Initialize(ItemData[] items)
    {
        this.items = items;
        text.text = (items[0].cost * level).ToString();
        level = 1;
    }
    private void Update()
    {
        if (level > items.Length)
        {
            text.text = "MAX";
            text.color = Color.red;
            _button.interactable = false;
        }
        else if (items[level].cost * level > playerStats.stanleyWeight)
        {
            text.text = (items[level].cost * level).ToString();
            _button.interactable = false;
            text.color = Color.grey;
        }
        else
        {
            text.text = (items[level + 1].cost * level).ToString();
            _button.interactable = true;
            text.color = Color.white;
        }
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}
