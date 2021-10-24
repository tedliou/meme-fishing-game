using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Canvas fishingInformationCanvas;
    public Image baitTimerFill;
    public GameObject popupPrefab;
    public static GameManager instance;

    public State state;
    public PlayerStats playerStats;

    public void Awake()
    {
        instance = this;

        playerStats.fishingRodLevel = 0;
        playerStats.stanleyPower = 1;
        playerStats.additionalLifetime = 0;
        playerStats.stanleyWeight = 1000;
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
public enum State
{
    Throwing,
    Fishing,
    Standby,
}