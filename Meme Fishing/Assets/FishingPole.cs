using UnityEngine;

public class FishingPole : MonoBehaviour
{
    public GameObject fishingPole;
    public Transform poleTip;
    public Transform bait;
    public LineRenderer lr;

    public PlayerStats playerStats;

    private Rigidbody2D _baitRB;
    private State currentState;
    private Camera cam;
    void Start()
    {
        _baitRB = bait.GetComponent<Rigidbody2D>();
        currentState = State.Throwing;
        cam = Camera.main;
    }

    public void ReelIn()
    {
        if (currentState == State.Throwing) { return; }
        _baitRB.AddForce((poleTip.position - bait.position).normalized * playerStats.reelPower);
    }
    public void StartPullingBait()
    {
        cam.orthographicSize = 8;
    }
    public void UpdatePullingBait()
    {
        float dist = Mathf.Min(PoleToMouseDist, playerStats.lineLength);
        bait.position = PoleToMouseDir * dist + (Vector2)poleTip.position;
    }
    public void EndPullingBait()
    {

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
            if (Input.GetMouseButtonDown(1))
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
    public Vector2 GetMousePos => cam.ScreenToWorldPoint(Input.mousePosition);
    public Vector2 PoleToMouseDir => (GetMousePos - (Vector2)poleTip.position).normalized;
    public float PoleToMouseDist => Vector2.Distance(GetMousePos, poleTip.position);
}
public enum State
{
    Throwing,
    Fishing,
}