using UnityEditor;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;

    [SerializeField] private MeshRenderer crouchingPlayer;
    [SerializeField] private CapsuleCollider crouchingPlayerCollider;

    [SerializeField] private MeshRenderer standingPlayer;
    [SerializeField] private CapsuleCollider standingPlayerCollider;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SO_playerMovementSettings.State == PlayerState.Crouching)
        {
            SO_playerMovementSettings.IsCrouchJumping = true;
            StandUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnCrouchInput();
        }
    }*/

    public void PlayerCrouched()
    {
        //I don't excatly know if you wanted to have crouch while jump or something else
        if (SO_playerMovementSettings.State == PlayerState.Crouching)
        {
           StandUp();
        }

        else
        {
            OnCrouchInput();
        }
    }

    private void OnCrouchInput()
    {
        if (SO_playerMovementSettings.State == PlayerState.Airborne) return;

        if (SO_playerMovementSettings.State == PlayerState.Crouching)
        {
            StandUp();
            return;
        }

        Crouch();
    }

    public void StandUp()
    {
        SO_playerMovementSettings.State = PlayerState.Standing;

        crouchingPlayer.enabled = false;
        crouchingPlayerCollider.enabled = false;
        standingPlayer.enabled = true;
        standingPlayerCollider.enabled = true;
    }

    private void Crouch()
    {
        SO_playerMovementSettings.State = PlayerState.Crouching;

        crouchingPlayer.enabled = true;
        crouchingPlayerCollider.enabled = true;
        standingPlayer.enabled = false;
        standingPlayerCollider.enabled = false;
    }
}
