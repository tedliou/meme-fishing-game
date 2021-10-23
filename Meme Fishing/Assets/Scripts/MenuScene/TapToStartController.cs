using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TapToStartController : MonoBehaviour
{
    public string gameScene = "GameScene";
    public FadeInCanvas fadeInCanvas;

    public void EnterGame () {
        fadeInCanvas.FadeOut(() => SceneManager.LoadSceneAsync(gameScene));
    }
}
