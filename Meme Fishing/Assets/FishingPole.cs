using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FishingPole : MonoBehaviour
{
    public GameObject fishingPole;
    public Transform poleTip;
    public Transform bait;
    public LineRenderer lr;

    public PlayerStats playerStats;

    private Rigidbody2D _baitRB;
    public State currentState;
    private Camera cam;
    void Start()
    {
        _baitRB = bait.GetComponent<Rigidbody2D>();
        currentState = State.Throwing;
        cam = Camera.main;
        cam.orthographicSize = 8;
    }

    public void ReelIn()
    {
        if (currentState == State.Throwing) { return; }
        _baitRB.AddForce((poleTip.position - bait.position).normalized * playerStats.reelPower);
    }
    public void EndFishingState()
    {
        currentState = State.Throwing;
        bait.transform.localPosition = Vector3.zero;
        _baitRB.velocity = Vector2.zero;
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);
    }
    public void StartPullingBait()
    {
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);
    }
    public void UpdatePullingBait()
    {
        bait.position = PoleToMouseDir * GetLinePulledDistance + (Vector2)poleTip.position;
    }

    public void EndPullingBait()
    {
        float power = GetLinePulledDistance / playerStats.lineLength * playerStats.stanleyPower;
        _baitRB.AddForce(-PoleToMouseDir * power);
        fishingPole.transform.DORotate(new Vector3(0, 0, -45), 0.5f).SetEase(Ease.InFlash);

        currentState = State.Standby;
        StartCoroutine(Delay(2f, () => currentState = State.Fishing));
    }


    // Update is called once per frame
    void Update()
    {
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
            if (PoleToBaitDist <= 1f)
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