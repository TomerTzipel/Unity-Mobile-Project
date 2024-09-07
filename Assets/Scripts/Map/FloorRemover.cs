using UnityEngine;

public class FloorRemover : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private UpdatedObstacleManager obstaclesManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            other.transform.position = SO_MapSettings.RespawnPoint;
            SO_MapSettings.LastTile = other.gameObject;

            obstaclesManager.CheckObstaclesSpawn();
        }
    }
}
