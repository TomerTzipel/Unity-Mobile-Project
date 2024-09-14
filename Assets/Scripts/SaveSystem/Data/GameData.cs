using System;

[Serializable]
public class GameData
{
    public PlayerData PlayerData;
    public MapData MapData;

    public GameData() 
    {
        PlayerData = new PlayerData();
        MapData = new MapData();
    }
}
