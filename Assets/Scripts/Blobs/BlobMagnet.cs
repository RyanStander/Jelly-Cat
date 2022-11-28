using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Blobs
{
    public class BlobMagnet : MonoBehaviour
    {
        [Header("Sphere cast values")] [SerializeField]
        private bool showGizmo;
        [SerializeField] private float radius = 0.6f;


        [Header("Blob values")] [SerializeField]
        private float attractionSpeed;

        private Transform target;
        private Transform blobTransform;

        //Check if it collides with the player, if it does

        private void Start()
        {
            blobTransform = transform;
        }
        
        private void FixedUpdate()
        {
            CheckForPlayer();

            MoveToTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                target = other.gameObject.transform;
            }
        }

        private void CheckForPlayer()
        {
            var didHit = Physics.SphereCast(blobTransform.position, radius, Vector3.zero, out var hit);

            if (!didHit)
                return;
            
            Debug.Log("hit");
            
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                target = hit.collider.gameObject.transform;
            }
        }

        private void MoveToTarget()
        {
            if (target == null) return;

            var targetLocation = Vector3.MoveTowards(blobTransform.position, target.position, attractionSpeed);
            blobTransform.position = targetLocation;
        }
        
        private void OnDrawGizmos()
        {
            if (!showGizmo)
                return;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}