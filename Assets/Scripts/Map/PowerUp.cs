using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PowerUpType
{
    Heal,Invulnerability,Shield
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpSettings powerUpSettings;
    [SerializeField] private PowerUpType type;

    [SerializeField] private SpawningManager _manager;

    public bool WasCollected { get; set; } = false;
    public SpawningManager Manager { get { return _manager; } set { _manager = value; } }

    public PowerUpType Type { get { return type; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerUpSettings.CurrentPowerUp = type;
            WasCollected = true;
            _manager.ReturnPowerUpToPool(this);
        }
    }

}
