using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject notificationPrefab;

    public Queue<FishAIProfile> Work { get; set; } = new Queue<FishAIProfile>();

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
            yield return gameObj.GetComponent<ResultCanvas>().Play(data);
            Destroy(gameObj);
            yield return null;
        }
    }
}
