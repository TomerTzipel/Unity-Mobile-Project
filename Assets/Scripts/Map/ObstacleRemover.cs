using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRemover : MonoBehaviour
{
    [SerializeField] private ObstaclesManager obstaclesManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstaclesManager.ReturnObstacleToPool(other.gameObject);
        }
    }
}
