using UnityEngine;

/// <summary>
/// Script that manages the movement of the JellyCat player.
/// </summary>
public class JellyCatController : MonoBehaviour
{
    [SerializeField] private LayerMask enviornmentLayer;
    [SerializeField] private GameObject jellyCore;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] [Range(0, 10)] private float gravityStrength = 10;
    [SerializeField] [Range(0.25f, 1)] private float wallDetectionBufferDistance = 0.5f;
    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private bool attachedToWall;

    /// <summary>
    /// Called once every frame.
    /// </summary>
    private void Update()
    {
        // Set & reset relevant data for the movement.
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Handle logic relating to the players wall climbing.
        HandleForWallClimb();

        // Handle XZ movement.
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * movementSpeed));
        if (move != Vector3.zero)
        {
            jellyCore.transform.forward = move;
        }

        // Handle the player jumping & height position.
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravityStrength);
        }
        playerVelocity.y -= gravityStrength * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Handle logic related to a player attaching to and climbing walls.
    /// </summary>
    private void HandleForWallClimb()
    {
        Vector3 origin = transform.position + controller.center;
        Collider[] colliders = Physics.OverlapSphere(origin, controller.radius + wallDetectionBufferDistance, enviornmentLayer.value);
        
        foreach (var item in colliders)
        {
            // Physics.ClosestPoint can only be used with a BoxCollider, SphereCollider, CapsuleCollider and a convex MeshCollider.
            if(item is SphereCollider || item is CapsuleCollider || item is BoxCollider || item.IsConvexMesh())
            {
                Debug.DrawLine(origin, item.ClosestPoint(origin), CustomColors.DarkOrange);
            }
            else
            {
                Debug.DrawLine(origin, item.ClosestPointOnBounds(origin), CustomColors.DarkOrange);
            }
        }
    }

    /// <summary>
    /// Visually debug relevant player data.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere around the player core for its wall detection range.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, controller.radius + wallDetectionBufferDistance);

        // Draw a green sphere around the player to visualize its location.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, controller.radius);
    }
}