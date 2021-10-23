using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class FishingPole : MonoBehaviour
{
    public GameObject fishingPole;
    public Transform poleTip;
    public Transform bait;
    public LineRenderer lr;
    public Image collectionTimer;

    public PlayerStats playerStats;

    private Rigidbody2D _baitRB;
    public State currentState;
    private Camera cam;
    private float timeValidZone;
    void Start()
    {
        _baitRB = bait.GetComponent<Rigidbody2D>();
        currentState = State.Throwing;
        cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.orthographicSize = 8;
        collectionTimer.fillAmount = 0;

        StartPullingBait();
    }

    public void ReelIn()
    {
        if (currentState == State.Throwing) { return; }
        _baitRB.AddForce((poleTip.position - bait.position).normalized * playerStats.reelPower);
    }
    public void EndFishingState()
    {
        currentState = State.Standby;
        StartCoroutine(Delay(1f, () => currentState = State.Throwing));
        bait.transform.localPosition = Vector3.zero;
        _baitRB.velocity = Vector2.zero;
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);

        collectionTimer.fillAmount = 0;
    }
    public void StartPullingBait()
    {
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);

        collectionTimer.fillAmount = 0;

        _baitRB.mass = playerStats.mass;
        _baitRB.gravityScale = playerStats.gravity;
    }
    public void UpdatePullingBait()
    {
        bait.position = PoleToMouseDir * GetLinePulledDistance + (Vector2)poleTip.position;
    }

    public void EndPullingBait()
    {
        float power = GetLinePulledDistance / playerStats.lineLength * playerStats.stanleyPower;
        _baitRB.AddForce(-PoleToMouseDir);
        fishingPole.transform.DORotate(new Vector3(0, 0, -45), 0.5f).SetEase(Ease.InFlash);
        StartCoroutine(Delay(0.5f, () => _baitRB.AddForce(-PoleToMouseDir * power, ForceMode2D.Impulse)));

        currentState = State.Standby;
        StartCoroutine(Delay(2f, () => currentState = State.Fishing));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { EndFishingState(); }

        if (currentState == State.Throwing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPullingBait();
            }
            else if (Input.GetMouseButton(0))
            {
                UpdatePullingBait();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndPullingBait();
            }
        }
        else if (currentState == State.Fishing)
        {
            if (PoleToBaitDist <= 6f)
            {
                timeValidZone += Time.deltaTime;
                collectionTimer.fillAmount = timeValidZone / 2f;
                if (timeValidZone > 2f)
                {
                    EndFishingState();
                }
            }
            else if (timeValidZone > 0)
            {
                timeValidZone = Mathf.Max(0, timeValidZone -= Time.deltaTime * 5);
                collectionTimer.fillAmount = timeValidZone / 2f;
            }

            if (bait.transform.position.x < -8f)
            {
                EndFishingState();
            }

            if (Input.GetMouseButton(1))
            {
                ReelIn();
            }

        }
    }
    private void LateUpdate()
    {
        lr.SetPosition(0, poleTip.position);
        lr.SetPosition(1, bait.position);

        cam.transform.position = Vector3.Lerp((bait.position + fishingPole.transform.position) / 2 + new Vector3(0, 0, -10), cam.transform.position, 0.05f);
        cam.orthographicSize = Mathf.Clamp((bait.position.x + Mathf.Max(-bait.position.y, 0)) / 2, 6, 20);
    }
    private IEnumerator Delay(float time, System.Action OnComplete)
    {
        yield return new WaitForSeconds(time);
        OnComplete.Invoke();
    }
    public Vector2 GetMousePos => cam.ScreenToWorldPoint(Input.mousePosition);
    public Vector2 PoleToMouseDir => (GetMousePos - (Vector2)poleTip.position).normalized;
    public float PoleToMouseDist => Vector2.Distance(GetMousePos, poleTip.position);
    public float PoleToBaitDist => Vector2.Distance(bait.position, poleTip.position);
    public float GetLinePulledDistance => Mathf.Min(PoleToMouseDist, playerStats.lineLength);
}
public enum State
{
    Throwing,
    Fishing,
    Standby,
}