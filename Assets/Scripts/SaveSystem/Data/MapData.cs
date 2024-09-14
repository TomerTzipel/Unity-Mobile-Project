using System;
using System.Collections.Generic;


[Serializable]
public class MapData
{
    private TileData[] _tilesData;
    public MapData()
    {
        _tilesData = new TileData[31];
        for (int i = 0; i < 31; i++) 
        {
            _tilesData[i] = new TileData();
        }
    }

    public TileData[] TileData
    {
        get { return _tilesData; }
    }
}
