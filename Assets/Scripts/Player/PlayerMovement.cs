using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private Rigidbody rb;


    private void Awake()
    {
        SO_playerMovementSettings.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnMoveInput(Vector2.right);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            OnMoveInput(Vector2.left); 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnMoveInput(Vector2.left);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            OnMoveInput(Vector2.right);
        }
    }

    private void OnMoveInput(Vector2 MovementDirection)
    {
        SO_playerMovementSettings.MoveInputDetected = true;
        SO_playerMovementSettings.MovementDirection += MovementDirection;
    }

    private void FixedUpdate()
    {
        
        if (SO_playerMovementSettings.MoveInputDetected)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = SO_playerMovementSettings.Velocity;

        Vector3 VelocityChange = targetVelocity - currentVelocity;
        VelocityChange = new Vector3(VelocityChange.x, 0, VelocityChange.z);

        rb.AddForce(VelocityChange,ForceMode.VelocityChange);
        SO_playerMovementSettings.MoveInputDetected = false;
    }
}
