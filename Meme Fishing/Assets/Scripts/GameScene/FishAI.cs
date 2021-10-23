using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI :MonoBehaviour {
    public FishAIProfile currentProfile;
    public ResultCanvas resultCanvas;

    [Header("Component")]
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer fishBody;

    [Header("SFX")]
    public AudioClip catchOne;
    public AudioClip catchTwo;
    public AudioClip free;
    public AudioClip success;

    public static int catchCount = 0;

    private Transform catchBait;
    public bool isCatach = false;
    private AudioSource audioSource;
    private Vector2 direction = Vector2.right;
    private float minMoveDist = 6;
    private float maxMoveDist = 20;
    private float currentSetupDist;
    private float currentDist;
    private float speed = 2;

    private void Start () {
        audioSource = GetComponent<AudioSource>();
        ChangeDirection();

        StartCoroutine(LoopSprite());

        fishBody.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private IEnumerator LoopSprite () {
        var index = 0;
        while (true)
        {
            spriteRenderer.sprite = currentProfile.sprites[index];
            index++;
            if (index > currentProfile.sprites.Length - 1)
            {
                index = 0;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private void FixedUpdate () {
        if (isCatach)
        {
            transform.position = catchBait.position;
        }
        else
        {
            var old = transform.position;
            transform.localPosition += (Vector3)direction.normalized * speed * Time.fixedDeltaTime;
            currentDist += Vector2.Distance(transform.position, old);
            if (currentDist >= currentSetupDist) ChangeDirection();
            if (( transform.position - old ).x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.name == "Bait")
        {
            FollowBait(collision.transform);
            Free();
        }
    }

    private void OnCollisionExit2D (Collision2D collision) {
        if (collision.gameObject.name == "Bait")
        {
            Free();
        }
    }

    private void OnCollisionStay2D (Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Gatcha"))
        {
            catchCount++;
            Resources.FindObjectsOfTypeAll<ResultCanvas>()[0].Play(currentProfile, gameObject);
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.CompareTag("Water"))
        {
            Debug.Log("Water");
            ChangeDirectionToDown();
            //Free();
        }
    }

    private void ChangeDirection () {
        if (isCatach) return;
        currentDist = 0;
        currentSetupDist = Random.Range(minMoveDist, maxMoveDist);
        direction = new Vector2(Random.Range(-45, 45), Random.Range(-45, 45));
    }

    private void ChangeDirectionToDown () {
        if (isCatach) return;
        currentDist = 0;
        currentSetupDist = maxMoveDist;
        direction = new Vector2(0, -1);
    }

    public void Catch () {
        audioSource.clip = catchOne;
        audioSource.Play();
    }

    public void CatchDouble () {
        audioSource.clip = catchTwo;
        audioSource.Play();
    }

    public void Free () {
        audioSource.clip = free;
        audioSource.Play();
    }

    public void Success () {
        audioSource.clip = success;
        audioSource.Play();
    }

    public void FollowBait (Transform bait) {
        catchBait = bait;
        isCatach = true;
    }

    public void ReleaseFromBait () {
        isCatach = false;
    }
}


