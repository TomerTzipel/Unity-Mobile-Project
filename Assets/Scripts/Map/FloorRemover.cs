using UnityEngine;

public class FloorRemover : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    
    private void OnTriggerEnter(Collider other)
    {

        other.transform.position = SO_MapSettings.RespawnPoint;
        SO_MapSettings.LastTile = other.gameObject;
    }
}
