using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelSystem/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int index;
    [SerializeField] private int scoreToFinish; 
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private float speedMultiplier;

    [SerializeField] private int obstacleDifficulityMax;
    [SerializeField] private int obstacleDifficulityMin;

    [SerializeField] private int obstacleSpawnRate;
    [SerializeField] private int powerUpSpawnRate;
    
    public int Index { get { return index; } }
    public int ScoreToFinish { get { return scoreToFinish; } }
    public float ScoreMultiplier { get { return scoreMultiplier; } }
    public float SpeedMultiplier { get { return speedMultiplier; } }
    public int ObstacleDifficulityMax { get { return obstacleDifficulityMax; } }
    public int ObstacleDifficulityMin { get { return obstacleDifficulityMin; } }
    public int ObstacleSpawnRate { get { return obstacleSpawnRate; } }
    public int PowerUpSpawnRate { get { return powerUpSpawnRate; } }
}
