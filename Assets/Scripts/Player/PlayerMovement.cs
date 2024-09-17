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
            OnMoveRightInput();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            OnCancelMoveRightInput();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnMoveLeftInput();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            OnCancelMoveLeftInput();
        }
    }
    
    public void OnMoveLeftInput()
    {
        ReadMoveInput(Vector2.left);
    }

    public void OnCancelMoveLeftInput() 
    {
        ReadMoveInput(Vector2.right);
    }

    public void OnMoveRightInput()
    {
        ReadMoveInput(Vector2.right);
    }

    public void OnCancelMoveRightInput()
    {
        ReadMoveInput(Vector2.left);
    }

    private void ReadMoveInput(Vector2 MovementDirection)
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
