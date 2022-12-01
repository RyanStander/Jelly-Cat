using System;
using Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelCompletionZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            EventManager.currentManager.AddEvent(new PlayerAttemptLevelCompletion());
    }
}