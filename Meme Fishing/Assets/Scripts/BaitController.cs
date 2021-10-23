using UnityEngine;

public class BaitController : MonoBehaviour
{
    public float lifeTime = 0;
    public float baseLifeTime = 0;
    private Rigidbody2D rb;
    private bool _touchedWater;
    public bool caughtFish;
    private FishingPole _fishingPole;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _touchedWater = false;
        caughtFish = false;
    }
    private void Update()
    {
        if (_touchedWater == true && !caughtFish)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                _fishingPole.EndFishingState();
            }
            GameManager.instance.UpdateBaitTimer(lifeTime, baseLifeTime);
        }
    }
    public void OnCatchFish()
    {
        caughtFish = true;
    }
    public void OnCollect()
    {
        Debug.LogError("Collecting is not implemented yet!");
    }
    public void BaitInitialize(float lifeTime, FishingPole fishingPole)
    {
        this.lifeTime = lifeTime;
        baseLifeTime = lifeTime;
        _touchedWater = false;
        _fishingPole = fishingPole;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lifeTime > 0) { if (collision.CompareTag("Water")) { _touchedWater = true; } }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) { _touchedWater = false; }
    }
}
