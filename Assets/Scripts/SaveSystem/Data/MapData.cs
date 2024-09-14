using System;
using System.Collections.Generic;


[Serializable]
public class MapData
{
    public TileData[] TilesData;

    public int ObstaclesCounter;
    public int PowerUpCounter;
    public int LastTileIndex;

    public MapData()
    {
        TilesData = new TileData[31];
        for (int i = 0; i < 31; i++) 
        {
            TilesData[i] = new TileData();
        }
        ObstaclesCounter = 0;
        PowerUpCounter = 0;
        LastTileIndex = 30;
    }

}
