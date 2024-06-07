using UnityEngine;

public class FloorRemover : MonoBehaviour
{
    [SerializeField] Vector3 respawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = respawnPoint;
    }
}
