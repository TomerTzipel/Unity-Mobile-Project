using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private AnimationHandler animationHandler;

    [SerializeField] private Collider crouchingPlayerCollider;
    [SerializeField] private Collider standingPlayerCollider;

    [SerializeField] private float slideDuration;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnSlideInput();
        }
    }

    public void OnJumpInput()
    {
        if (SO_playerMovementSettings.State == PlayerState.Sliding)
        {
            //Slide Jump
            SO_playerMovementSettings.IsSlideJumping = true;
            StopAllCoroutines();
            StandUp();
        }
    }

    public void OnSlideInput()
    {
        if (SO_playerMovementSettings.State == PlayerState.Airborne) return;

        if (SO_playerMovementSettings.State == PlayerState.Sliding) return;
 
        Slide();
    }

    private void StandUp()
    {
        SO_playerMovementSettings.State = PlayerState.Standing;

        crouchingPlayerCollider.enabled = false;
        standingPlayerCollider.enabled = true;
    }

    private void Slide()
    {
        SO_playerMovementSettings.State = PlayerState.Sliding;
        animationHandler.OnSlideInput();

        crouchingPlayerCollider.enabled = true;
        standingPlayerCollider.enabled = false;
        StartCoroutine(SlideDuration());
    }

    private IEnumerator SlideDuration()
    {
        yield return new WaitForSeconds(slideDuration);
        StandUp();
    }
}
