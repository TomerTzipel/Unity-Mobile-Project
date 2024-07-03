using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideMovement : MonoBehaviour
{
    [SerializeField] private PlayerCrouch playerCrouch;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private PlayerMovementSettings SO_MovementSettings;


    private Vector2 Direction
    {
        get { return SO_MovementSettings.MovementDirection; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnRightInput()
    {
        if (Direction.x > 0) return;

        playerMovement.OnMoveRightInput();

    }

    private void OnLeftInput()
    {
        if (Direction.x < 0) return;

        playerMovement.OnMoveLeftInput();

    }

    private void OnJumpInput()
    {
        playerJump.OnJumpInput();
    }

    private void OnCrouchInput()
    {
        playerCrouch.OnCrouchInput();
    }



}
