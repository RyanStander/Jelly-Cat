using Events;
using UnityEngine;
using UnityEngine.UI;
using EventType = Events.EventType;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Slider scoreSlider;
        [SerializeField] private float sliderSpeed=0.1f;
        private float currentScore;
        private float currentDisplayScore;

        private void FixedUpdate() => ScoreLerp();

        private void ScoreLerp()
        {
            currentDisplayScore = Mathf.Lerp(currentDisplayScore, currentScore, sliderSpeed);
            scoreSlider.value = currentDisplayScore;
        }
        
        #region OnEvents

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
                //sets slider and current score
                scoreSlider.maxValue = setScoreStats.TotalScore/100;
                currentScore = setScoreStats.StartingScore/100;
                currentDisplayScore = currentScore;
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
                currentScore = updateCurrentScore.CurrentScore/100;
            }
            else
            {
                Debug.LogWarning("EventType UpdateCurrentScore does not match eventData of type UpdateCurrentScore");
            }
        }

        #endregion
    }
}
