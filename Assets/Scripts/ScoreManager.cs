using System;
using Events;
using UI;
using UnityEngine;
using EventType = Events.EventType;

/// <summary>
/// Holds the data of the current score and the required amount to pass a level
/// </summary>
public class ScoreManager : MonoBehaviour
{
    private float requiredScore;
    private float currentScore;
    private float timeLimit;

    [SerializeField] private Timer timer;

    private float DetermineFinalScore()
    {
        //Determine the time score, if it is lower than 0, the player will receive no score
        var timeScore = timeLimit - timer.GetDuration();
        if (timeScore < 0)
        {
            timeScore = 0;
        }

        var finalScore = currentScore + timeScore;

        return finalScore;
    }

    #region OnEvents

    private void OnValidate()
    {
        if (timer == null)
            timer = FindObjectOfType<Timer>();
    }

    private void OnEnable()
    {
        EventManager.currentManager.Subscribe(EventType.SetScoreStats, OnSetScoreStats);
        EventManager.currentManager.Subscribe(EventType.UpdateCurrentScore, OnUpdateCurrentScore);
        EventManager.currentManager.Subscribe(EventType.PlayerAttemptLevelCompletion, OnPlayerAttemptLevelCompletion);
    }

    private void OnDisable()
    {
        EventManager.currentManager.Unsubscribe(EventType.SetScoreStats, OnSetScoreStats);
        EventManager.currentManager.Unsubscribe(EventType.UpdateCurrentScore, OnUpdateCurrentScore);
        EventManager.currentManager.Unsubscribe(EventType.PlayerAttemptLevelCompletion, OnPlayerAttemptLevelCompletion);
    }

    private void OnPlayerAttemptLevelCompletion(EventData eventData)
    {
        if (eventData is PlayerAttemptLevelCompletion)
        {
            if (currentScore >= requiredScore)
            {
                EventManager.currentManager.AddEvent(new DidPlayerCompleteLevel(true, DetermineFinalScore()));
            }
        }
        else
        {
            Debug.LogWarning(
                "EventType PlayerAttemptLevelCompletion does not match eventData of type PlayerAttemptLevelCompletion");
        }
    }

    private void OnSetScoreStats(EventData eventData)
    {
        if (eventData is SetScoreStats setScoreStats)
        {
            //sets current and required score
            requiredScore = setScoreStats.RequiredScore;
            currentScore = setScoreStats.StartingScore;
            timeLimit = setScoreStats.TimeLimit;
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
            currentScore = updateCurrentScore.CurrentScore / 100;
        }
        else
        {
            Debug.LogWarning("EventType UpdateCurrentScore does not match eventData of type UpdateCurrentScore");
        }
    }

    #endregion
}