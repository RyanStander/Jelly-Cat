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
                //divide by 100 to get decimal percentile
                var scaleIncrease = sendBlobScore.BlobScore / 100;
                
                var playerTransform = transform;
                var playerScale = playerTransform.localScale;

                playerScale += Vector3.one * scaleIncrease;
                
                playerTransform.localScale = playerScale;

            }
            else
            {
                Debug.LogError("You used wrong event data");
            }
        }
    }
}