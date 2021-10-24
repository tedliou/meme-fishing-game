using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Image baitTimerFill;
    public TMP_Text gameTimeText;
    public GameObject popupPrefab;
    public static GameManager instance;

    public State state;
    public PlayerStats playerStats;
    private float _gameTime;

    public void Awake()
    {
        instance = this;

        playerStats.fishingRodLevel = 0;
        playerStats.stanleyPower = 0.5f;
        playerStats.additionalLifetime = 0;
        playerStats.stanleyWeight = 1000;
    }
    private void Update()
    {
        _gameTime += Time.deltaTime;
        gameTimeText.text = Utils.FormatTimeToMinutes(_gameTime);
    }
    public void UpdateBaitTimer(float time, float maxTime)
    {
        baitTimerFill.rectTransform.sizeDelta = new Vector2(Mathf.Min(maxTime * 50, 1500), 30);
        baitTimerFill.fillAmount = time / maxTime;
    }
    public void AddWeight(int weight)
    {
        playerStats.stanleyWeight += weight;
        SpawnPopup(Random.insideUnitCircle, "+" + weight.ToString(), Color.green);
    }
    public void SpawnPopup(Vector2 pos, string text, Color color)
    {
        Popup popup = Instantiate(popupPrefab).GetComponent<Popup>();
        popup.Initialize(text, color);
        popup.transform.position = pos;
    }
}
public static class Utils
{
    public static string FormatTimeToHours(float time)
    {
        int hours = Mathf.RoundToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
    public static string FormatTimeToMinutes(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
public enum State
{
    Throwing,
    Fishing,
    Standby,
}