using UnityEngine;

public enum PlayerState
{
    Standing, Airborne, Crouching
}

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/Player/MovementSettings")]
public class PlayerMovementSettings : ScriptableObject
{
    public PlayerState State { get; set; }

    [SerializeField] private float speed;
    public Vector2 MovementDirection { get; set; }
    public bool MoveInputDetected { get; set; }

    [SerializeField] private float jumpForce;
    [SerializeField] private float crouchJumpForce;

    public bool IsCrouchJumping { get; set; }

    public void Initialize()
    {
        State = PlayerState.Standing;
        MovementDirection = Vector2.zero;
        MoveInputDetected = false;
        IsCrouchJumping = false;
    }

    public Vector3 Velocity
    {
        get { return speed * MovementDirection; }
    }

    public Vector3 JumpVelocity
    {
        get { return jumpForce * Vector2.up; }
    }
    public Vector3 CrouchJumpVelocity
    {
        get { return crouchJumpForce * Vector2.up; }
    }
}
