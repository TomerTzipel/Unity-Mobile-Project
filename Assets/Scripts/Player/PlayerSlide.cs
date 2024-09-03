using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;

    [SerializeField] private MeshRenderer crouchingPlayer;
    [SerializeField] private Collider crouchingPlayerCollider;

    [SerializeField] private MeshRenderer standingPlayer;
    [SerializeField] private Collider standingPlayerCollider;

    [SerializeField] private float slideDuration;

    private Coroutine slideDurationCoroutine;

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
        Debug.Log($"From Slide:{SO_playerMovementSettings.State}");
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

        crouchingPlayer.enabled = false;
        crouchingPlayerCollider.enabled = false;
        standingPlayer.enabled = true;
        standingPlayerCollider.enabled = true;
    }

    private void Slide()
    {
        SO_playerMovementSettings.State = PlayerState.Sliding;

        crouchingPlayer.enabled = true;
        crouchingPlayerCollider.enabled = true;
        standingPlayer.enabled = false;
        standingPlayerCollider.enabled = false;
        StartCoroutine(SlideDuration());
    }

    private IEnumerator SlideDuration()
    {
        yield return new WaitForSeconds(slideDuration);
        StandUp();
    }
}
