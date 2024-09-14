using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class TileData
{
    public Vector3 Position;

    public ObstacleType LeftObstacle;
    public ObstacleType MidObstacle;
    public ObstacleType RightObstacle;

    public bool IsTherePowerUp;
    public PowerUpType PowerUpType;
    public float PowerUpX;
    public float PowerUpY;

    public TileData()
    {
        Position = Vector3.zero;

        LeftObstacle = 0;
        MidObstacle = 0;
        RightObstacle = 0;

        IsTherePowerUp = false;
        PowerUpType = 0;
        PowerUpX = 0;
        PowerUpY = 0;
    } 
}
