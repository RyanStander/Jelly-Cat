using UnityEngine;

/// <summary>
/// Script that manages the movement of the JellyCat player.
/// </summary>
public class JellyCatController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] [Range(0, 10)] private float gravityStrength = 9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

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

        // Handle XZ movement.
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * movementSpeed));
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Handle the player jumping & height position.
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravityStrength);
        }
        playerVelocity.y -= gravityStrength * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}