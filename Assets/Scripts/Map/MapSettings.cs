using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "MapSettings", menuName = "ScriptableObjects/Map/MapSettings")]
public class MapSettings : ScriptableObject
{
    [SerializeField] private float tilesSpeed;
    public GameObject LastTile { get; set; }

    private float moveSpeedOffset
    {
        get { return tilesSpeed / 50f; }
    }

    public Vector3 RespawnPoint
    {
        get 
        {
            return new Vector3(LastTile.transform.position.x, LastTile.transform.position.y, LastTile.transform.position.z + LastTile.transform.localScale.z - moveSpeedOffset); 
        }
    }

    public float TilesSpeed
    {
        get { return tilesSpeed; }
    }
}
