using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PowerUpType
{
    Heal,Invulnerability,Shield
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpSettings powerUpSettings;
    [SerializeField] PowerUpType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerUpSettings.CurrentPowerUp = type;
            //Return to pool
        }
    }
}
