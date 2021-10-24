using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject notificationPrefab;

    public Queue<(FishAIProfile, GameObject)> Work { get; set; } = new Queue<(FishAIProfile, GameObject)>();

    private void Start () {
        StartCoroutine(Worker());
    }

    private IEnumerator Worker () {
        while (true)
        {
            if (Work.Count == 0)
            {
                yield return null;
                continue;
            }

            // Show Notificaiton
            var data = Work.Dequeue();
            var gameObj = Instantiate(notificationPrefab);
            yield return gameObj.GetComponent<ResultCanvas>().Play(data.Item1, data.Item2);
            Destroy(gameObj);
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
