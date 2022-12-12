using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(BoxCollider))]
    public class SwapCameraOnEnter : MonoBehaviour
    {
        [SerializeField] private GameObject cameraToSwapTo;
        [SerializeField] private GameObject[] camerasToDisable;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
           
            foreach (var cameraToDisable in camerasToDisable)
            {
                cameraToDisable.SetActive(false);
            }
            cameraToSwapTo.SetActive(true);
        }
    }
}
