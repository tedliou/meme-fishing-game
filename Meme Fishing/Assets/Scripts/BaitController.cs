using UnityEngine;

public class BaitController : MonoBehaviour
{
    public float lifeTime = 0;
    public float baseLifeTime = 0;
    private Rigidbody2D rb;
    private bool _touchedWater;
    public FishAIProfile fish;
    private FishingPole _fishingPole;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _touchedWater = false;
    }
    private void Update()
    {
        if (_touchedWater == true && fish == null)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                _fishingPole.EndFishingState();
            }
            GameManager.instance.UpdateBaitTimer(lifeTime, baseLifeTime);
        }
    }
    public void ResetBait()
    {
        GameManager.instance.UpdateBaitTimer(0, 1);
        fish = null;
    }
    public void OnCatchFish(FishAIProfile fish)
    {
        this.fish = fish;
    }
    public void BaitInitialize(float lifeTime, FishingPole fishingPole)
    {
        this.lifeTime = lifeTime;
        baseLifeTime = lifeTime;
        _touchedWater = false;
        _fishingPole = fishingPole;
        fish = null;
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
