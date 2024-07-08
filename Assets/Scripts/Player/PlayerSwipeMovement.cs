using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipeMovement : MonoBehaviour
{
    [SerializeField] private PlayerCrouch playerCrouch;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private PlayerMovementSettings SO_MovementSettings;

    Vector2 initialTouchPosition;
    float touchStartTime;
    bool isSwipping = false;

    private Vector2 Direction
    {
        get { return SO_MovementSettings.MovementDirection; }
    }

    private float SwipeMaxTime
    {
        get { return SO_MovementSettings.SwipeMaxTime; }
    }
    private float SwipeMinDistanceSqr
    {
        get { return Mathf.Pow(SO_MovementSettings.SwipeMinDistance,2); }
    }
    private float SwipeAngleThreshold
    {
        get { return SO_MovementSettings.SwipeAngleThreshold; }
    }

    void Update()
    {
        if (Input.touchCount != 1) return;

        Touch swipeTouch = Input.GetTouch(0);

        if (swipeTouch.phase == TouchPhase.Began)
        {
            initialTouchPosition = swipeTouch.position;
            touchStartTime = Time.time;
            isSwipping = true;
            return;
        }

        if(swipeTouch.phase == TouchPhase.Ended || swipeTouch.phase == TouchPhase.Canceled)
        {
            if (!isSwipping) return;

            Vector2 touchCurrnetPosition = swipeTouch.position; 
            float touchEndTime = Time.time;

            if (touchEndTime - touchStartTime > SwipeMaxTime)
            {
                Debug.Log("fail time");
                return;
            } 
           

            Vector2 swipeVector = touchCurrnetPosition - initialTouchPosition;

            if (swipeVector.sqrMagnitude < SwipeMinDistanceSqr)
            {
                Debug.Log("fail dis");
                return;
            }

            float swipeAngle = Vector2.SignedAngle(Vector2.right, swipeVector);

            if (swipeAngle < 0f + SwipeAngleThreshold && swipeAngle > 0f - SwipeAngleThreshold)
            {
                Debug.Log("right");
                OnRightInput();
                return;
            }

            if ((swipeAngle < -180f + SwipeAngleThreshold && swipeAngle > -180f) || swipeAngle > 180f - SwipeAngleThreshold && swipeAngle < 180f)
            {
                Debug.Log("left");
                OnLeftInput();
                return;
            }

            if (swipeAngle < 90f + SwipeAngleThreshold && swipeAngle > 90f - SwipeAngleThreshold)
            {
                Debug.Log("jump");
                OnJumpInput();
                return;
            }

            if (swipeAngle < -90f + SwipeAngleThreshold && swipeAngle > -90f - SwipeAngleThreshold)
            {
                Debug.Log("crouch");
                OnCrouchInput();
                return;
            }
        }
    }

    private void OnRightInput()
    {
        if (Direction.x > 0) return;

        playerMovement.OnMoveRightInput();

        if (Direction.x == 0)
        {
            playerMovement.OnMoveRightInput();
        }

    }

    private void OnLeftInput()
    {
        if (Direction.x < 0) return;

        playerMovement.OnMoveLeftInput(); 
        
        if (Direction.x == 0)
        {
            playerMovement.OnMoveLeftInput();
        }
    }

    private void OnJumpInput()
    {
        playerCrouch.OnJumpInput();
        playerJump.OnJumpInput();
    }

    private void OnCrouchInput()
    {
        playerCrouch.OnCrouchInput();
    }



}
