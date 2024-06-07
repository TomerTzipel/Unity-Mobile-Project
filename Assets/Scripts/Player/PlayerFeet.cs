using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings SO_playerMovementSettings;
    [SerializeField] private BoxCollider groundSensor;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("WASHERE");
            groundSensor.enabled = true;
        }
    }


   
}
