using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teach : MonoBehaviour
{
    public Transform[] pos;

    private SpriteRenderer render;

    private void Start () {
        render = GetComponent<SpriteRenderer>();
        StartCoroutine(TechWork());
    }

    private IEnumerator TechWork () {
        while (true)
        {
            render.DOFade(1, .5f);
            transform.position = pos[0].position;
            yield return new WaitForSecondsRealtime(1);
            transform.DOMove(pos[1].position, 1);
            yield return new WaitForSecondsRealtime(1);
            render.DOFade(0, .5f);


            yield return new WaitForSecondsRealtime(5);
        }
    }
}
