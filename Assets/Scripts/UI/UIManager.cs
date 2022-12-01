using System;
using Events;
using TMPro;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject loseScreen;
        [Header("Win Screen")]
        [SerializeField] private GameObject winScreen;

        [SerializeField] private TextMeshProUGUI scoreText;
        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.DidPlayerCompleteLevel,OnPlayerCompletedLevel);
            EventManager.currentManager.Subscribe(EventType.PlayerDied,OnPlayerDied);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.DidPlayerCompleteLevel,OnPlayerCompletedLevel);
            EventManager.currentManager.Unsubscribe(EventType.PlayerDied,OnPlayerDied);
        }

        private void OnPlayerCompletedLevel(EventData eventData)
        {
            if (eventData is PlayerCompletedLevel didPlayerCompleteLevel)
            {
                hud.SetActive(false);
                winScreen.SetActive(true);
                scoreText.text = "You achieved a score of: "+didPlayerCompleteLevel.FinalScore;
            }
            else
            {
                Debug.Log("I really want to not do this");
            }
        }

        private void OnPlayerDied(EventData eventData)
        {
            if (eventData is PlayerDied)
            {
                hud.SetActive(false);
                loseScreen.SetActive(true);
            }
            else
            {
                Debug.Log("Im lazy.");
            }
        }
    }
}