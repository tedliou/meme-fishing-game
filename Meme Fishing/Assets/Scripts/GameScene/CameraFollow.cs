using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public FishingPole fishingPole;
    public Transform fishingBait;

    private Vector3 _originPos;
    private Camera _camera;

    private void Start () {
        _camera = GetComponent<Camera>();
        _originPos = transform.position;
    }

    private void Update () {
        if (fishingPole.currentState == State.Fishing)
        {
            _camera.transform.position = fishingBait.position + new Vector3(0, 0, -10);
        }
        else
        {
            _camera.transform.position = _originPos;
        }
    }
}
