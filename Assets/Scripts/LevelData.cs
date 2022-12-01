using Events;
using UnityEngine;

/// <summary>
/// Holds all data that will be used and managed for the level
/// </summary>
public class LevelData : MonoBehaviour
{
    [SerializeField] private float maxScore=200;
    [SerializeField] private float startingScore=100;
    [SerializeField] private float levelTime=300;

    private void Awake()
    {
        EventManager.currentManager.AddEvent(new SetScoreStats(maxScore,startingScore));
        EventManager.currentManager.AddEvent(new SetTime(levelTime));
    }
}