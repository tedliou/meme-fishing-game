using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInCanvas : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void OnEnable () {
        if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 2);
    }

    public void FadeOut (UnityAction callback) {
        _canvasGroup.DOFade(0, 2).OnComplete(callback.Invoke);
    }
}
