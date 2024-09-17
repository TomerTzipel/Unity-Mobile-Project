using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource jumpSFX;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }
    }

    public void OnJumpInput()
    {
        Jump();
    }

    private void Jump()
    {
        if (SO_playerMovementSettings.State == PlayerState.Airborne) return;

        jumpSFX.Play();
        animationHandler.OnJumpInput();
        Vector3 JumpVelocity = SO_playerMovementSettings.JumpVelocity;

        if (SO_playerMovementSettings.IsSlideJumping)
        {
            JumpVelocity = SO_playerMovementSettings.CrouchJumpVelocity;
            SO_playerMovementSettings.IsSlideJumping = false;
        }

        rb.AddForce(JumpVelocity, ForceMode.VelocityChange);

        SO_playerMovementSettings.State = PlayerState.Airborne;  
    }
}
