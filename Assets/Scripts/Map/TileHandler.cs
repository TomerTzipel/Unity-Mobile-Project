using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{

    [SerializeField] private int _dataIndex;

    [SerializeField] private Obstacle _leftObstacle;
    [SerializeField] private Obstacle _midObstacle;
    [SerializeField] private Obstacle _rightObstacle;

    private PowerUp _powerUp;

    public void ReleaseToPools(SpawningManager spawningManager)
    {
        if (_leftObstacle != null) 
        {
            Debug.Log("Release Left");
            spawningManager.ReturnObstacleToPool(_leftObstacle);
            _leftObstacle = null;
        }

        if (_midObstacle != null)
        {
            Debug.Log("Release mid");
            spawningManager.ReturnObstacleToPool(_midObstacle);
            _midObstacle = null;
        }

        if (_rightObstacle != null)
        {
            Debug.Log("Release right");
            spawningManager.ReturnObstacleToPool(_rightObstacle);
            _rightObstacle = null;
        }

        if (_powerUp != null) 
        { 
            spawningManager.ReturnPowerUpToPool(_powerUp); 
            _powerUp = null;
        }
    }
    public void AddObstacle(Obstacle obstacle,Lane lane)
    {
        switch (lane)
        {
            case Lane.Left:
                _leftObstacle = obstacle;
                break;
            case Lane.Mid:
                _midObstacle = obstacle;
                break;
            case Lane.Right:
                _rightObstacle = obstacle;
                break;
        }

    }

    public void AddPowerUp(PowerUp powerUp)
    {
        _powerUp = powerUp;
    }

}
