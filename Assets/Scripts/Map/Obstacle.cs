using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public UpdatedObstacleManager Manager { get; set; }

    [SerializeField] private ObstacleType type;

    public ObstacleType Type { get { return type; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleRemover"))
        {
            Manager.ReturnObstacleToPool(this);
        }
    }
}

