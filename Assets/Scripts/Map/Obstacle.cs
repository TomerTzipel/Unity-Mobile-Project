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
    public ObstacleType Type { get { return type; } }
}

