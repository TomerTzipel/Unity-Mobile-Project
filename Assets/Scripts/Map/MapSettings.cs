using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "MapSettings", menuName = "ScriptableObjects/Map/MapSettings")]
public class MapSettings : ScriptableObject
{
    [SerializeField] private float tilesSpeed;

    public float speedMultiplier { get; set; }

    public GameObject LastTile { get; set; }

    private float moveSpeedOffset
    {
        get { return TilesSpeed / 50f; }
    }

    public Vector3 RespawnPoint
    {
        get 
        {
            float z = LastTile.transform.position.z + LastTile.transform.localScale.z - moveSpeedOffset;
            return new Vector3(LastTile.transform.position.x, LastTile.transform.position.y, z); 
        }
    }

    public float TilesSpeed
    {
        get { return tilesSpeed * speedMultiplier; }
    }
}
