using System;
using Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelCompletionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            EventManager.currentManager.AddEvent(new PlayerAttemptLevelCompletion());
    }
}