using System;

[Serializable]
public class GameData
{
    private PlayerData _playerData;
    private MapData _mapData;

    public GameData() 
    {
        _playerData = new PlayerData();
        _mapData = new MapData();
    }

    public PlayerData PlayerData {  get { return _playerData; } set { _playerData = value; } }
    public MapData MapData { get { return _mapData; } set { _mapData = value; } }
}
