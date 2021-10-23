using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class FishingPole : MonoBehaviour
{
    public GameObject fishingPole;
    public GameObject fishingRod;
    public Transform bait;
    public LineRenderer lr;
    public Image collectionTimer;

    public PlayerStats playerStats;

    private Rigidbody2D _baitRB;
    private Transform _poleTip;

    private Camera cam;
    private float timeValidZone;

    void Start()
    {
        _baitRB = bait.GetComponent<Rigidbody2D>();
        _poleTip = fishingRod.transform.GetChild(0);
        cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.orthographicSize = 8;
        collectionTimer.fillAmount = 0;

        ResetThrowingState();
    }
    public void SwitchBait(ItemData newBait)
    {
        Destroy(bait.gameObject);

        bait = Instantiate(newBait.itemPrefab, fishingPole.transform).transform;
        _baitRB = bait.GetComponent<Rigidbody2D>();

        ResetThrowingState();
    }
    public void SwitchRod(ItemData newRod)
    {
        Destroy(fishingRod);
        fishingRod = Instantiate(newRod.itemPrefab, fishingPole.transform);
        _poleTip = fishingRod.transform.GetChild(0);
    }
    public void ReelIn()
    {
        if (GameManager.instance.state == State.Throwing) { return; }
        _baitRB.AddForce((_poleTip.position - bait.position).normalized * playerStats.reelPower);
    }
    public void EndFishingState()
    {
        GameManager.instance.state = State.Standby;
        StartCoroutine(Delay(0.5f, () => GameManager.instance.state = State.Throwing));
        ResetThrowingState();

        bait.GetComponent<BaitController>().OnCollect();

        collectionTimer.fillAmount = 0;
    }

    private void ResetThrowingState()
    {
        bait.transform.localPosition = _poleTip.position;
        _baitRB.velocity = Vector2.zero;
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);
    }

    public void StartPullingBait()
    {
        fishingPole.transform.DORotate(new Vector3(0, 0, 75), 0.2f);

        _baitRB.mass = playerStats.selectedBait.mass;
        _baitRB.gravityScale = playerStats.selectedBait.gravity;
    }
    public void UpdatePullingBait()
    {
        bait.position = PoleToMouseDir * GetLinePulledDistance + (Vector2)_poleTip.position;
        collectionTimer.fillAmount = 0;
    }

    public void EndPullingBait()
    {
        float power = GetLinePulledDistance / playerStats.lineLength * playerStats.stanleyPower;
        _baitRB.AddForce(-PoleToBaitDir * power, ForceMode2D.Impulse);
        fishingPole.transform.DORotate(new Vector3(0, 0, -45), 0.6f).SetEase(Ease.InFlash);
        StartCoroutine(Delay(0.5f, () => _baitRB.AddForce(-PoleToBaitDir * power, ForceMode2D.Impulse)));

        GameManager.instance.state = State.Standby;
        StartCoroutine(Delay(2f, () => GameManager.instance.state = State.Fishing));

        collectionTimer.fillAmount = 0;

        bait.GetComponent<BaitController>().BaitInitialize(playerStats.LifeTime, this);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { ResetThrowingState(); }

        if (GameManager.instance.state == State.Throwing)
        {
            if (_poleTip == null || _baitRB == null) { return; }

            //if (GetMousePos.x > -5f)
            //{
            //    ResetThrowingState();
            //    validityZoneIndicator.SetActive(true);
            //    return;
            //}
            //else { validityZoneIndicator.SetActive(false); }

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
        else if (GameManager.instance.state == State.Fishing)
        {
            if (bait == null)
            {
                ResetThrowingState();
            }
            if (PoleToBaitDist <= 5f)
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
        lr.SetPosition(0, _poleTip.position);
        lr.SetPosition(1, bait.position);

        cam.transform.position = Vector3.Lerp((bait.position + fishingPole.transform.position) / 2 + new Vector3(0, 0, -10), cam.transform.position, 0.9f);
        cam.orthographicSize = Mathf.Clamp((bait.position.x + Mathf.Max(-bait.position.y, 0)) / 3f, 6, 100);
    }
    private IEnumerator Delay(float time, System.Action OnComplete)
    {
        yield return new WaitForSeconds(time);
        OnComplete.Invoke();
    }
    public Vector2 GetMousePos => cam.ScreenToWorldPoint(Input.mousePosition);
    public Vector2 PoleToMouseDir => (GetMousePos - (Vector2)_poleTip.position).normalized;
    public Vector2 PoleToBaitDir => (bait.position - _poleTip.position).normalized;
    public float PoleToMouseDist => Vector2.Distance(GetMousePos, _poleTip.position);
    public float PoleToBaitDist => Vector2.Distance(bait.position, _poleTip.position);
    public float GetLinePulledDistance => Mathf.Min(PoleToMouseDist, playerStats.lineLength);
}