using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData
{
	private Vector3 _position;

    private ObstacleType _leftObstacle;
    private ObstacleType _midObstacle;
    private ObstacleType _rightObstacle;

    private bool _isTherePowerUp;
    private PowerUpType _powerUpType;
    private float _powerUpX;
    private float _powerUpY;

    public Vector3 Position
    {
		get { return _position; }
		set { _position = value; }
	}

    public ObstacleType LeftObstacle
	{
		get { return _leftObstacle; }
		set { _leftObstacle = value; }
	}

    public ObstacleType MidObstacle
    {
        get { return _midObstacle; }
        set { _midObstacle = value; }
    }

    public ObstacleType RightObstacle
    {
        get { return _rightObstacle; }
        set { _rightObstacle = value; }
    }

    public bool IsTherePowerUp
    {
        get { return _isTherePowerUp; }
        set { _isTherePowerUp = value; }
    }

    public PowerUpType PowerUpType
    {
        get { return _powerUpType; }
        set { _powerUpType = value; }
    }

    public float PowerUpX
    {
        get { return _powerUpX; }
        set { _powerUpX = value; }
    }
    public float PowerUpY
    {
        get { return _powerUpY; }
        set { _powerUpY = value; }
    }
}
