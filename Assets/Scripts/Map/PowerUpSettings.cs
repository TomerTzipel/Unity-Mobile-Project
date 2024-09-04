using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSettings", menuName = "ScriptableObjects/Map/PowerUpSettings")]
public class PowerUpSettings : ScriptableObject
{
    public PowerUpType CurrentPowerUp{ get; set; }
}
