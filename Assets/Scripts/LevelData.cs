using Events;
using UnityEngine;

/// <summary>
/// Holds all data that will be used and managed for the level
/// </summary>
public class LevelData : MonoBehaviour
{
    [Tooltip("This is the intended score to succeed, you can go higher...")] [SerializeField]
    private float requiredScore = 200;

    [Tooltip("This is the score you start with, also affects character starting scale")] [SerializeField]
    private float startingScore = 100;

    [Tooltip("After this amount of time has passed, the player will not receive any points for their speed")]
    [SerializeField]
    private float timeLimit = 120;

    private void Awake()
    {
        EventManager.currentManager.AddEvent(new SetScoreStats(requiredScore, startingScore,timeLimit));
    }
}