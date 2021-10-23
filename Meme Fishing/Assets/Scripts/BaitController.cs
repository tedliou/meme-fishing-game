using UnityEngine;

public class BaitController : MonoBehaviour
{
    public float lifeTime = 0;
    private Rigidbody2D rb;
    private bool _touchedWater;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _touchedWater = false;
    }
    private void Update()
    {
        if (_touchedWater == true)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {

            }
        }
    }
    public void BaitInitialize(float lifeTime)
    {
        this.lifeTime = lifeTime;
        _touchedWater = false;
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
