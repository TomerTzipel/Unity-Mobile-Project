using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Pool;

public enum Lane
{
    Left, Mid, Right
}
public enum ObstacleType
{
    Open, FullyBlocked, JumpOnly, SlideOnly, SlideJumpOnly, AnyJump, NotJump
}
public class UpdatedObstacleManager : MonoBehaviour
{

    private int obstacleDifficulityMax = 4;
    private int obstacleDifficulityMin = 3;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private Obstacle FullObstaclePrefab;
    [SerializeField] private Obstacle AnyJumpObstaclePrefab;
    [SerializeField] private Obstacle JumpOnlyObstaclePrefab;
    [SerializeField] private Obstacle SlideJumpObstaclePrefab;
    [SerializeField] private Obstacle SlideObstaclePrefab;

    private int obstacleSpawnRate = 6; 
    private int _obstacleSpawnCounter = 0;

    private Dictionary<ObstacleType, ObstaclePool> _obstaclePools = new Dictionary<ObstacleType, ObstaclePool>();

    private void Awake()
    {
        _obstaclePools.Add(ObstacleType.FullyBlocked,new ObstaclePool(FullObstaclePrefab,this));
        _obstaclePools.Add(ObstacleType.AnyJump, new ObstaclePool(AnyJumpObstaclePrefab, this));
        _obstaclePools.Add(ObstacleType.JumpOnly, new ObstaclePool(JumpOnlyObstaclePrefab, this));
        _obstaclePools.Add(ObstacleType.SlideJumpOnly, new ObstaclePool(SlideJumpObstaclePrefab, this));
        _obstaclePools.Add(ObstacleType.SlideOnly, new ObstaclePool(SlideObstaclePrefab, this));
    }

    private GameObject LastTile
    {
        get { return gameManager.LastTile; }
    }

    public void ReturnObstacleToPool(Obstacle obstacle)
    {
        _obstaclePools[obstacle.Type].ReturnObstacle(obstacle);
    }
    public void CheckObstaclesSpawn()
    {
        _obstacleSpawnCounter++;
        if (_obstacleSpawnCounter == obstacleSpawnRate)
        {
            _obstacleSpawnCounter = 0;
            SpawnObstacles();
        }
    }
    private void SpawnObstacles()
    {
        ObstacleType chosenType1, chosenType2, chosenType3;
        ChooseObstacles(out chosenType1, out chosenType2, out chosenType3);

        Obstacle obstacle1 = GetObstacleByType(chosenType1);
        Obstacle obstacle2 = GetObstacleByType(chosenType2);
        Obstacle obstacle3 = GetObstacleByType(chosenType3);

        if (obstacle1 != null) PlaceObstacle(obstacle1, Lane.Left);
        if (obstacle2 != null) PlaceObstacle(obstacle2, Lane.Mid);
        if (obstacle3 != null) PlaceObstacle(obstacle3, Lane.Right);
    }

    private Obstacle GetObstacleByType(ObstacleType type)
    {
        if (type == ObstacleType.Open) return null;
        return _obstaclePools[type].GetObstacle();
    }

    private void PlaceObstacle(Obstacle obstacle,Lane lane)
    {
        float obstacleX = 0f;
        switch (lane)
        {
            case Lane.Left:
                obstacleX -= (1f / 3f);
                break;
            case Lane.Right:
                obstacleX += (1f / 3f);
                break;
        }
        float obstacleY = 0;

        switch (obstacle.Type)
        {
            case ObstacleType.SlideOnly:
                obstacleY = 3.25f;
                break;
            case ObstacleType.FullyBlocked:
                obstacleY = 4.5f;
                break;
            case ObstacleType.SlideJumpOnly:
                obstacleY = 3f;
                break;
            case ObstacleType.JumpOnly:
                obstacleY = 1.5f;
                break;
            case ObstacleType.AnyJump:
                obstacleY = 1.9f;
                break;
        }

        obstacle.transform.parent = LastTile.transform;
        obstacle.transform.localPosition = new Vector3(obstacleX, obstacleY, 0f);
        obstacle.gameObject.SetActive(true);
    }
    private void ChooseObstacles(out ObstacleType type1, out ObstacleType type2, out ObstacleType type3)
    {
        type1 = ObstacleType.FullyBlocked;
        type2 = ObstacleType.FullyBlocked;
        type3 = ObstacleType.FullyBlocked;

        bool found = false;

        while (!found)
        {
            type1 = (ObstacleType)Random.Range(0, 6);
            type2 = (ObstacleType)Random.Range(0, 6);
            type3 = (ObstacleType)Random.Range(0, 6);

            found = AreObstaclesLegel(type1, type2, type3);
        }
    }

    private bool AreObstaclesLegel(ObstacleType type1, ObstacleType type2, ObstacleType type3)
    {
        int ObstaclesValue = ObstacleValue(type1) + ObstacleValue(type2) + ObstacleValue(type3);

        return ObstaclesValue >= obstacleDifficulityMin && ObstaclesValue <= obstacleDifficulityMax;
    }

    private int ObstacleValue(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.Open:
                return 3;
            case ObstacleType.FullyBlocked:
                return 0;
            case ObstacleType.JumpOnly:
                return 1;
            case ObstacleType.SlideOnly:
                return 1;
            case ObstacleType.SlideJumpOnly:
                return 1;
            case ObstacleType.AnyJump:
                return 2;
        }

        Debug.LogError("No proper type was passed");
        return -1;
    }
   
    private Obstacle SpawnObstacle(ObstacleType type)
    {
        return _obstaclePools[type].GetObstacle();
    }
    private void PlaceObstacle(GameObject obstacle, ObstacleType type, Lane lane)
    {
        //check where to place it by type and lane
    }


}
