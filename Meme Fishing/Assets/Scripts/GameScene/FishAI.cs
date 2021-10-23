using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private float minMoveDist = 6;
    private float maxMoveDist = 20;
    private float currentSetupDist;
    private float currentDist;
    private float speed = 6;
    private Rigidbody2D _rigidbody2D;

    private void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        ChangeDirection();
        //transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
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
        //_rigidbody2D.MovePosition((Vector2)transform.position + direction * Time.fixedDeltaTime);
        //_rigidbody2D.add
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
