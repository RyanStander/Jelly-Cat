using System;
using Events;
using UnityEngine;

namespace Blobs
{
    public class BlobStats : MonoBehaviour
    {
        [Tooltip("This should be kept to a small amount as this is percent increase")][SerializeField]
        private float blobScore=10;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            EventManager.currentManager.AddEvent(new SendBlobScore(blobScore));
            Destroy(transform.parent.gameObject);
        }
    }
}
