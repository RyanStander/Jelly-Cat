using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Blobs
{
    public class BlobMagnet : MonoBehaviour
    {
        [Header("Blob values")] [SerializeField]
        private float attractionSpeed;

        private Transform target;
        private Transform blobTransform;

        private void Start()
        {
            blobTransform = transform;
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        //Assign player as target if they enter the magnet range
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                target = other.gameObject.transform;
        }

        //Moves to target if it is assigned
        private void MoveToTarget()
        {
            if (target == null) return;

            var targetLocation = Vector3.MoveTowards(blobTransform.position, target.position, attractionSpeed);
            blobTransform.position = targetLocation;
        }
    }
}