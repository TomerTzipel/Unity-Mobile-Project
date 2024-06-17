using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] BoxCollider playerGroundSensor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            SO_playerMovementSettings.State = PlayerState.Standing;
            playerGroundSensor.enabled = false;
        }
    }
}