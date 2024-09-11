using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Open, FullyBlocked, JumpOnly, SlideOnly, SlideJumpOnly, AnyJump, NotJump
}

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType type;
    [SerializeField] private SpawningManager _manager;
    public SpawningManager Manager { get { return _manager; } set { _manager = value; } }

    public ObstacleType Type { get { return type; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleRemover"))
        {
            _manager.ReturnObstacleToPool(this);
        }
    }
}

