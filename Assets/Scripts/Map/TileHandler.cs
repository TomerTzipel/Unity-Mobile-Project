using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour, ISaveable
{

    [SerializeField] private int _dataIndex;
    [SerializeField] private LoadSettings _loadSettings;
    [SerializeField] SpawningManager _spawningManager;

    private Obstacle _leftObstacle;
    private Obstacle _midObstacle;
    private Obstacle _rightObstacle;

    [SerializeField] private ObstacleType _leftStartingObstacleType;
    [SerializeField] private ObstacleType _midStartingObstacleType;
    [SerializeField] private ObstacleType _rightStartingObstacleType;

    private PowerUp _powerUp;

    public int DataIndex {  get { return _dataIndex; } }

    private void Awake()
    {
        if (!_loadSettings.LoadFromSave)
        {
            _spawningManager.LoadObstaclesToTile(_leftStartingObstacleType, _midStartingObstacleType, _rightStartingObstacleType, this);
        }
        
    }

    public void ReleaseToPools()
    {
        if (_leftObstacle != null) 
        {
            _spawningManager.ReturnObstacleToPool(_leftObstacle);
            _leftObstacle = null;
        }

        if (_midObstacle != null)
        {
            _spawningManager.ReturnObstacleToPool(_midObstacle);
            _midObstacle = null;
        }

        if (_rightObstacle != null)
        {
            _spawningManager.ReturnObstacleToPool(_rightObstacle);
            _rightObstacle = null;
        }

        if (_powerUp != null) 
        {
            _spawningManager.ReturnPowerUpToPool(_powerUp); 
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

    public void LoadData(GameData data)
    {
        TileData tileData = data.MapData.TilesData[_dataIndex];

        transform.position = tileData.Position;

        _spawningManager.LoadObstaclesToTile(tileData.LeftObstacle, tileData.MidObstacle, tileData.RightObstacle,this);

        if (tileData.IsTherePowerUp)
        {
            _spawningManager.LoadPowerUpToTile(tileData.PowerUpType,tileData.PowerUpX,tileData.PowerUpY, this);
        }

    }
    public void SaveData(ref GameData data)
    {
        TileData tileData = new TileData();

        tileData.Position = transform.position;

        if(_leftObstacle != null) tileData.LeftObstacle = _leftObstacle.Type;
        if (_midObstacle != null) tileData.MidObstacle = _midObstacle.Type;
        if (_rightObstacle != null) tileData.RightObstacle = _rightObstacle.Type;

        tileData.IsTherePowerUp = _powerUp != null;

        if(_powerUp != null)
        {
            tileData.PowerUpType = _powerUp.Type;
            tileData.PowerUpX = _powerUp.transform.localPosition.x;
            tileData.PowerUpY = _powerUp.transform.localPosition.y;
        }

        data.MapData.TilesData[_dataIndex] = tileData;
    }

}
