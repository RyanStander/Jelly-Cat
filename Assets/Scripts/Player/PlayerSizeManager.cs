using System;
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
        [SerializeField] private Cloth playerCloth;
        private float playerScore=100;

        private void UpdatePlayerScale()
        {
            //divide by 100 to get decimal percentile
            var scaleIncrease = playerScore / 100;

            transform.localScale = Vector3.one * scaleIncrease;

            playerCloth.enabled = false;
            playerCloth.enabled = true;
        }

        #region OnEvents

        private void OnValidate()
        {
            if (playerCloth == null)
                GetComponentInChildren<Cloth>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.SendBlobScore, OnSendBlobScore);
            EventManager.currentManager.Subscribe(EventType.SetScoreStats,OnSetScoreStats);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.SendBlobScore, OnSendBlobScore);
            EventManager.currentManager.Unsubscribe(EventType.SetScoreStats,OnSetScoreStats);
        }
        
        private void OnSetScoreStats(EventData eventData)
        {
            if (eventData is SetScoreStats setScoreStats)
            {
                playerScore = setScoreStats.StartingScore;
                UpdatePlayerScale();
            }
            else
            {
                Debug.LogError("You used wrong event data");
            }
        }
        
        private void OnSendBlobScore(EventData eventData)
        {
            if (eventData is SendBlobScore sendBlobScore)
            {
                playerScore += sendBlobScore.BlobScore;
                UpdatePlayerScale();
                EventManager.currentManager.AddEvent(new UpdateCurrentScore(playerScore));
            }
            else
            {
                Debug.LogError("You used wrong event data");
            }
        }

        #endregion
    }
}