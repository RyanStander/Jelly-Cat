using Events;
using UnityEngine;
using EventType = Events.EventType;

namespace Player
{
    /// <summary>
    /// When the player absorbs a blob, it gains size.
    /// </summary>
    public class PlayerSizeManager : MonoBehaviour
    {
        private float playerScore=100;
        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.SendBlobScore, OnSendBlobScore);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.SendBlobScore, OnSendBlobScore);
        }

        private void OnSendBlobScore(EventData eventData)
        {
            if (eventData is SendBlobScore sendBlobScore)
            {
                playerScore += sendBlobScore.BlobScore;
                //divide by 100 to get decimal percentile
                var scaleIncrease = playerScore / 100;

                transform.localScale = Vector3.one * scaleIncrease;
            }
            else
            {
                Debug.LogError("You used wrong event data");
            }
        }
    }
}