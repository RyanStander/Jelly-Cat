    using System;
    using Events;
    using UnityEngine;

    //If player lands enters a collider of dead zone, they die
    [RequireComponent(typeof(BoxCollider))]
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                EventManager.currentManager.AddEvent(new PlayerDied());
        }
    }