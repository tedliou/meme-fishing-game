using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public MemeType type = MemeType.Stanley;
    public enum MemeType {
        Stanley = 0
    }

    public Sprite[] stanley;

    private Sprite[] currentSprite;
    private Vector2 direction = Vector2.right;
    private float minMoveDist = 6;
    private float maxMoveDist = 20;
    private float currentSetupDist;
    private float currentDist;
    private float speed = 6;
    private SpriteRenderer renderer;

    private void Start () {
        renderer = GetComponent<SpriteRenderer>();
        ChangeDirection();
        switch (type)
        {
            case MemeType.Stanley:
                currentSprite = stanley;
                break;
        }
        
        StartCoroutine(LoopSprite());
    }

    private IEnumerator LoopSprite () {
        var index = 0;
        while (true)
        {
            renderer.sprite = currentSprite[index];
            index++;
            if (index > currentSprite.Length - 1)
            {
                index = 0;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private void FixedUpdate () {
        var old = transform.position;
        transform.localPosition += (Vector3)direction.normalized * speed * Time.fixedDeltaTime;
        currentDist += Vector2.Distance(transform.position, old);
        if (currentDist >= currentSetupDist) ChangeDirection();
        if (( transform.position - old ).x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionStay2D (Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            ChangeDirection();
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.CompareTag("Water"))
        {
            Debug.Log("Water");
            ChangeDirectionToDown();
        }
    }

    private void ChangeDirection () {
        currentDist = 0;
        currentSetupDist = Random.Range(minMoveDist, maxMoveDist);
        direction = new Vector2(Random.Range(-45, 45), Random.Range(-45, 45));
    }

    private void ChangeDirectionToDown () {
        currentDist = 0;
        currentSetupDist = maxMoveDist;
        direction = new Vector2(0, -1);
    }
}