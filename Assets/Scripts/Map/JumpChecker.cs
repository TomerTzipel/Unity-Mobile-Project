using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChecker : MonoBehaviour
{


    [SerializeField] BoxCollider playerGroundSensor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerGroundSensor.enabled = true;
        }
    }
}
