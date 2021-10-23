using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    public Image image;
    public AudioSource audioSource;

    private FishAIProfile _profile;
    private GameObject _fishObj;

    private void Start () {
    }

    public void Play (FishAIProfile profile, GameObject fishObj) {
        _profile = profile;
        _fishObj = fishObj;
        if (_profile.sfx != null)
        {
            audioSource.clip = _profile.sfx;
            audioSource.Play();
        }
        gameObject.SetActive(true);
        StartCoroutine(LoopSprite());
    }

    public void Gatcha () {
        Destroy(_fishObj);
        _profile = null;
        _fishObj = null;
        StopCoroutine(LoopSprite());
        gameObject.SetActive(false);
    }

    private IEnumerator LoopSprite () {
        var index = 0;
        while (true)
        {
            if (_profile.sprites.Length <= 0) break;
            if (index > _profile.sprites.Length - 1)
            {
                index = 0;
            }
            image.sprite = _profile.sprites[index];
            index++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
