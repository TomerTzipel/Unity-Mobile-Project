using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TileCollisionHandler : MonoBehaviour
{
    [SerializeField] private TileHandler tileHandler;
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private SpawningManager spawningManager;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FloorRemover"))
        {
            transform.position = SO_MapSettings.RespawnPoint;
            SO_MapSettings.LastTile = tileHandler;
            tileHandler.ReleaseToPools();
            spawningManager.CheckObstaclesSpawn();
        }
    }

}
