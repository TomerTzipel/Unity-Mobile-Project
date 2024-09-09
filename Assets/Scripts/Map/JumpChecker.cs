using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChecker : MonoBehaviour
{
    [SerializeField] BoxCollider playerGroundSensor; 
    [SerializeField] private AnimationHandler animationHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerGroundSensor.enabled = true;
            animationHandler.SetGrounded(false);
        }
    }
}
