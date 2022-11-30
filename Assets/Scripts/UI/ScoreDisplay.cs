using System;
using Events;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Slider scoreSlider;
        

        private void OnValidate()
        {
            if (scoreSlider != null)
                scoreSlider= GetComponent<Slider>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.SetScoreStats,OnSetScoreStats);
            EventManager.currentManager.Subscribe(EventType.UpdateCurrentScore,OnUpdateCurrentScore);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.SetScoreStats,OnSetScoreStats);
            EventManager.currentManager.Unsubscribe(EventType.UpdateCurrentScore,OnUpdateCurrentScore);
        }

        private void OnSetScoreStats(EventData eventData)
        {
            if (eventData is SetScoreStats setScoreStats)
            {
                scoreSlider.maxValue = setScoreStats.TotalScore;
                scoreSlider.value = setScoreStats.StartingScore;
            }
            else
            {
                Debug.LogWarning("EventType SetScoreStats does not match eventData of type SetScoreStats");
            }
        }

        private void OnUpdateCurrentScore(EventData eventData)
        {
            if (eventData is UpdateCurrentScore updateCurrentScore)
            {
                scoreSlider.value = updateCurrentScore.CurrentScore;
            }
            else
            {
                Debug.LogWarning("EventType UpdateCurrentScore does not match eventData of type UpdateCurrentScore");
            }
        }
    }
}
