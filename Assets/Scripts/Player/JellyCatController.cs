using Extensions;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Script that manages the movement of the JellyCat player.
    /// </summary>
    public class JellyCatController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LayerMask enviornmentLayer;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Transform jellyCore;
        [SerializeField] private SphereCollider jellyCoreCollider;
        [SerializeField] private Transform directionIndicator;

        [Header("Player Movement")]
        [SerializeField] [Range(0, 10)] private float movementSpeed = 5f;
        [SerializeField] [Range(0, 10)] private float groundDrag = 2.5f;
        [SerializeField] [Range(0, 10)] private float climbDrag = 5f;
        [SerializeField] [Range(0, 10)] private float airDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float airbornMovementMultiplier = 0.25f;
        [SerializeField] [Range(0, 10)] private float climbDirectionGravity = 5;

        [Header("Player Jumping")]
        [SerializeField] [Range(0, 10)] private float jumpStrength = 6.5f;
        [SerializeField] [Range(0, 1)] private float jumpCooldown = 0.15f;
        private bool jumpRequested = false;
        private bool canJump = true;

        [Header("Miscellaneous")]
        [SerializeField] [Range(0.01f, 0.25f)] private float skinWidthBuffer = 0.1f;
        [SerializeField] private MovementState currentMovementState = MovementState.undefined;
        [SerializeField] private GameObject objectBeingClimbed;
        private Vector3 climbDirection = Vector3.down;
        private bool playerGrounded;
        private bool playerClimbing;
        private float horizontalInput = 0;
        private float verticalInput = 0;

        /// <summary>
        /// The movement states that are applicable to the player.
        /// </summary>
        private enum MovementState
        {
            grounded,
            climbing,
            groundClimb,
            airborn,
            undefined
        }

        /// <summary>
        /// Called once on the frame when a script is enabled.
        /// </summary>
        private void Start()
        {
            rigidBody.freezeRotation = true;
            canJump = true;
        }

        /// <summary>
        /// Called once every frame.
        /// </summary>
        private void Update()
        {
            SetMovementState();

            HandleInputs();

            HandleJumping();

            HandleMovement();

            HandleSpeed();
        }

        /// <summary>
        /// Set the current movement state based on the ground and climb conditions of the player.
        /// </summary>
        private void SetMovementState()
        {
            // Check for conditions.
            GroundCheck();
            WallClimbCheck();

            // Modify movement state based on current conditions.
            if (playerGrounded && playerClimbing)
            {
                currentMovementState = MovementState.groundClimb;
            }
            else if (playerGrounded)
            {
                currentMovementState = MovementState.grounded;
            }
            else if (playerClimbing)
            {
                currentMovementState = MovementState.climbing;
            }
            else
            {
                currentMovementState = MovementState.airborn;
            }
        }

        /// <summary>
        /// Check if the player is close enough to the ground to be considered grounded.
        /// </summary>
        private void GroundCheck()
        {
            if (Physics.Raycast(jellyCore.position, Vector3.down, out RaycastHit hit, jellyCoreCollider.radius + skinWidthBuffer, enviornmentLayer))
            {
                Debug.DrawLine(jellyCore.position, hit.point, CustomColors.DarkOrange);
                playerGrounded = true;
            }
            else
            {
                playerGrounded = false;
            }
        }

        /// <summary>
        /// Handle logic related to a player attaching to and climbing walls.
        /// </summary>
        private void WallClimbCheck()
        {
            Vector3 origin = jellyCore.position;
            Collider[] colliders = Physics.OverlapSphere(origin, jellyCoreCollider.radius + skinWidthBuffer, enviornmentLayer.value);

            objectBeingClimbed = null;
            playerClimbing = false;
            float closestDistance = float.MaxValue;
            float itemDistance;

            foreach (var item in colliders)
            {
                // Ignore item if not tagged as climbable.
                if (item.tag != "Climbable")
                {
                    continue;
                }

                Vector3 closestPoint;

                // Physics.ClosestPoint can only be used with a BoxCollider, SphereCollider, CapsuleCollider and a convex MeshCollider.
                if (item is SphereCollider || item is CapsuleCollider || item is BoxCollider || item.IsConvexMesh())
                {
                    closestPoint = item.ClosestPoint(origin);
                }
                else
                {
                    closestPoint = item.ClosestPointOnBounds(origin);
                }

                Debug.DrawLine(origin, closestPoint, CustomColors.DarkOrange);
                itemDistance = Vector3.Distance(origin, closestPoint);

                // Attach to climbable object if within range.
                if (itemDistance < closestDistance)
                {
                    closestDistance = itemDistance;
                    objectBeingClimbed = item.gameObject;
                    playerClimbing = true;
                    climbDirection = (closestPoint - origin).normalized;
                }
            }
        }

        /// <summary>
        /// Saves current user inputs to process in later handler methods.
        /// </summary>
        private void HandleInputs()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            // Handle the player jumping & height position.
            if (Input.GetButtonDown("Jump") && (playerGrounded || playerClimbing))
            {
                jumpRequested = true;
            }

            // Temporary visualization of flat inputs.
            if (new Vector3(horizontalInput, 0, verticalInput) != Vector3.zero)
            {
                directionIndicator.forward = new Vector3(horizontalInput, 0, verticalInput);
            }
        }

        /// <summary>
        /// Applies a jump force to the player if requested and able to.
        /// </summary>
        private void HandleJumping()
        {
            if (jumpRequested && canJump)
            {
                canJump = false;
                jumpRequested = false;

                Invoke(nameof(JumpReset), jumpCooldown);

                if (currentMovementState == MovementState.grounded)
                {
                    rigidBody.AddForce(-Vector3.down * jumpStrength, ForceMode.Impulse);
                }
                else if (currentMovementState == MovementState.climbing)
                {
                    // For now the jump is directly away from the climbed object.
                    rigidBody.AddForce(-climbDirection * jumpStrength, ForceMode.Impulse);
                }
            }
        }

        /// <summary>
        /// Reset boolean that dictates if a player can jump.
        /// </summary>
        private void JumpReset() => canJump = true;

        /// <summary>
        /// Uses the player inputs and moves them in a requested direction. 
        /// </summary>
        private void HandleMovement()
        {
            // Calculate the direction of movement.
            var movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            // Move the player based on current states.
            if (currentMovementState == MovementState.grounded || currentMovementState == MovementState.groundClimb)
            {
                rigidBody.AddForce(movementDirection * movementSpeed, ForceMode.Force);
            }
            // TODO: Implement functioning movement for any surface direction.
            else if (currentMovementState == MovementState.climbing)
            {
                Vector3 climbMovementDirection = Vector3.Reflect(-climbDirection * horizontalInput, Vector3.forward);
                movementDirection = new Vector3(climbMovementDirection.z, verticalInput, climbMovementDirection.x).normalized;
                rigidBody.AddForce(movementDirection * movementSpeed, ForceMode.Force);
            }
            else if (currentMovementState == MovementState.airborn)
            {
                rigidBody.AddForce(movementDirection * movementSpeed * airbornMovementMultiplier, ForceMode.Force);
            }

            // Apply gravity only if the player is not climbing.
            rigidBody.useGravity = currentMovementState != MovementState.climbing;

            // Apply artificial gravity based on the climb state.
            if (currentMovementState == MovementState.climbing)
            {
                rigidBody.AddForce(climbDirection * climbDirectionGravity, ForceMode.Acceleration);
            }
        }

        /// <summary>
        /// Apply drag and limits to the player speed.
        /// </summary>
        private void HandleSpeed()
        {
            // Apply drag force to the player based on their grounded condition.
            switch (currentMovementState)
            {
                case MovementState.grounded:
                    rigidBody.drag = groundDrag;
                    break;

                case MovementState.climbing:
                    rigidBody.drag = climbDrag;
                    break;

                case MovementState.groundClimb:
                    rigidBody.drag = groundDrag;
                    break;

                default:
                    rigidBody.drag = airDrag;
                    break;
            }

            // Limit speed to its intended maximum.
            Vector3 flattenedVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

            if (flattenedVelocity.magnitude > movementSpeed)
            {
                Vector3 clampedVelocity = flattenedVelocity.normalized * movementSpeed;
                rigidBody.velocity = new Vector3(clampedVelocity.x, rigidBody.velocity.y, clampedVelocity.z);
            }
        }

        /// <summary>
        /// Visually debug relevant player data.
        /// </summary>
        private void OnDrawGizmos()
        {
            // Draw a yellow sphere around the player core for its wall detection range.
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(jellyCore.position + jellyCoreCollider.center, jellyCoreCollider.radius + skinWidthBuffer);

            // Draw a red sphere around the player core to visualize its location.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(jellyCore.position + jellyCoreCollider.center, jellyCoreCollider.radius);
        }

        /// <summary>
        /// Autofill missing components on validation (if possible).
        /// </summary>
        private void OnValidate()
        {
            if (jellyCoreCollider == null)
            {
                jellyCoreCollider = jellyCore.GetComponent<SphereCollider>();
            }
        }
    }
}