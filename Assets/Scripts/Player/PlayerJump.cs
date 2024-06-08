using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private Rigidbody rb;

    public void PlayerJumped()
    {
        Jump();
    }

    private void Jump()
    {
        if (SO_playerMovementSettings.State == PlayerState.Airborne) return;

        Vector3 JumpVelocity = SO_playerMovementSettings.JumpVelocity;

        if (SO_playerMovementSettings.IsCrouchJumping)
        {
            JumpVelocity = SO_playerMovementSettings.CrouchJumpVelocity;
            SO_playerMovementSettings.IsCrouchJumping = false;
        }

        rb.AddForce(JumpVelocity, ForceMode.VelocityChange);

        SO_playerMovementSettings.State = PlayerState.Airborne;  
    }
}
