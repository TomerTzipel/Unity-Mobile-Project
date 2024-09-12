using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Pool;

public enum Lane
{
    Left, Mid, Right
}

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelSettings levelSettings;

    [SerializeField] private Obstacle fullObstaclePrefab;
    [SerializeField] private Obstacle anyJumpObstaclePrefab;
    [SerializeField] private Obstacle jumpOnlyObstaclePrefab;
    [SerializeField] private Obstacle slideJumpObstaclePrefab;
    [SerializeField] private Obstacle slideObstaclePrefab;

    private Dictionary<ObstacleType, GameObjectPool<Obstacle>> _obstaclePools = new Dictionary<ObstacleType, GameObjectPool<Obstacle>>();
    private int _obstacleSpawnCounter = 0;

    [SerializeField] private PowerUp healPrefab;
    [SerializeField] private PowerUp invulnerabilityPrefab;
    [SerializeField] private PowerUp shieldPrefab;

    private Dictionary<PowerUpType, GameObjectPool<PowerUp>> _powerUpPools = new Dictionary<PowerUpType, GameObjectPool<PowerUp>>();
    private int _powerUpSpawnCounter = 0;

    private int ObstacleDifficulityMax { get { return levelSettings.CurrentLevel.ObstacleDifficulityMax; } }
    private int ObstacleDifficulityMin { get { return levelSettings.CurrentLevel.ObstacleDifficulityMin; } }
    private int ObstacleSpawnRate { get { return levelSettings.CurrentLevel.ObstacleSpawnRate; } }
    private int PowerUpSpawnRate { get { return levelSettings.CurrentLevel.PowerUpSpawnRate; } }

    private void Awake()
    {

        if (levelSettings.CurrentLevel == null) levelSettings.SetCurrentLevel(0);

        _obstaclePools.Add(ObstacleType.FullyBlocked,new GameObjectPool<Obstacle>(fullObstaclePrefab));
        _obstaclePools.Add(ObstacleType.AnyJump, new GameObjectPool<Obstacle>(anyJumpObstaclePrefab));
        _obstaclePools.Add(ObstacleType.JumpOnly, new GameObjectPool<Obstacle>(jumpOnlyObstaclePrefab));
        _obstaclePools.Add(ObstacleType.SlideJumpOnly, new GameObjectPool<Obstacle>(slideJumpObstaclePrefab));
        _obstaclePools.Add(ObstacleType.SlideOnly, new GameObjectPool<Obstacle>(slideObstaclePrefab));

        _powerUpPools.Add(PowerUpType.Heal, new GameObjectPool<PowerUp>(healPrefab));
        _powerUpPools.Add(PowerUpType.Invulnerability, new GameObjectPool<PowerUp>(invulnerabilityPrefab));
        _powerUpPools.Add(PowerUpType.Shield, new GameObjectPool<PowerUp>(shieldPrefab));
    }

    private GameObject LastTile
    {
        get { return gameManager.LastTile; }
    }

    public void ReturnObstacleToPool(Obstacle obstacle)
    {
        _obstaclePools[obstacle.Type].ReturnItem(obstacle);
    }
    public void ReturnPowerUpToPool(PowerUp powerUp)
    {
        _powerUpPools[powerUp.Type].ReturnItem(powerUp);
    }
    public void CheckObstaclesSpawn()
    {
        _obstacleSpawnCounter++;
        if (_obstacleSpawnCounter == ObstacleSpawnRate)
        {
            _obstacleSpawnCounter = 0;
            SpawnObstacles();
        }
    }
    private void SpawnObstacles()
    {
        ObstacleType chosenTypeLeft, chosenTypeMid, chosenTypeRight;
        ChooseObstacles(out chosenTypeLeft, out chosenTypeMid, out chosenTypeRight);

        Obstacle obstacleLeft = GetObstacleByType(chosenTypeLeft);
        Obstacle obstacleMid = GetObstacleByType(chosenTypeMid);
        Obstacle obstacleRight = GetObstacleByType(chosenTypeRight);

        if (obstacleLeft != null) PlaceObstacle(obstacleLeft, Lane.Left);
        if (obstacleMid != null) PlaceObstacle(obstacleMid, Lane.Mid);
        if (obstacleRight != null) PlaceObstacle(obstacleRight, Lane.Right);

        _powerUpSpawnCounter++;
        Debug.Log(_powerUpSpawnCounter);
        if (_powerUpSpawnCounter == PowerUpSpawnRate)
        {
            _powerUpSpawnCounter = 0;

            SpawnPowerUp(chosenTypeLeft, chosenTypeMid, chosenTypeRight);
        }
    }
    private void SpawnPowerUp(ObstacleType obstacleTypeLeft, ObstacleType obstacleTypeMid, ObstacleType obstacleTypeRight)
    {
        PowerUpType typeToSpawn = (PowerUpType) Random.Range(0, 3);

        List<Lane> lanes = new() { Lane.Left, Lane.Mid, Lane.Right };

        bool isAvailable = false;
        Lane chosenLane = Lane.Left;
        while (lanes.Count != 0)
        {
            chosenLane = lanes[Random.Range(0, lanes.Count)];
            lanes.Remove(chosenLane);

            switch (chosenLane)
            {
                case Lane.Left:
                    isAvailable = IsLaneAvailable(obstacleTypeLeft);
                    break;
                case Lane.Mid:
                    isAvailable = IsLaneAvailable(obstacleTypeMid);
                    break;
                case Lane.Right:
                    isAvailable = IsLaneAvailable(obstacleTypeRight);
                    break;
            }

            if (isAvailable) break;
        }

        if (!isAvailable) return; //If non of the lanes is available we can't spawn a power up

        switch (chosenLane)
        {
            case Lane.Left:
                PlacePowerUp(typeToSpawn, obstacleTypeLeft, chosenLane);
                break;
            case Lane.Mid:
                PlacePowerUp(typeToSpawn, obstacleTypeMid, chosenLane);
                break;
            case Lane.Right:
                PlacePowerUp(typeToSpawn, obstacleTypeRight, chosenLane);
                break;
        }

    }

    private bool IsLaneAvailable(ObstacleType obstacleType)
    {
        if(obstacleType == ObstacleType.FullyBlocked) return false;

        return true;
    }

    private void PlacePowerUp(PowerUpType type,ObstacleType obstacleType, Lane lane)
    {
        PowerUp powerUp = _powerUpPools[type].GetItem();
        powerUp.Manager = this;

        float powerUpX = 0f;
        switch (lane)
        {
            case Lane.Left:
                powerUpX -= (1f / 3f);
                break;
            case Lane.Right:
                powerUpX += (1f / 3f);
                break;
        }

        float powerUpY = 0f;
        int heightMult;
        switch (obstacleType)
        {
            case ObstacleType.Open:
                heightMult = Random.Range(0, 3);
                if(heightMult == 0)
                {
                    powerUpY = 1f;
                }
                else
                {
                    powerUpY = heightMult * 5f;
                }
                break;

            case ObstacleType.JumpOnly:
                powerUpY = 5f;
                break;

            case ObstacleType.SlideOnly:
                powerUpY = 1f;
                break;

            case ObstacleType.SlideJumpOnly:
                powerUpY = 10f;
                break;
            case ObstacleType.AnyJump:
                heightMult = Random.Range(1, 3);
                powerUpY = heightMult * 5f;
                break;
        }

        powerUp.transform.parent = LastTile.transform;
        powerUp.transform.localPosition = new Vector3(powerUpX, powerUpY, 0f);
        powerUp.gameObject.SetActive(true);
    }

    private Obstacle GetObstacleByType(ObstacleType type)
    {
        if (type == ObstacleType.Open) return null;
        Obstacle obstacle = _obstaclePools[type].GetItem();
        obstacle.Manager = this;
        return obstacle;
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

        return ObstaclesValue >= ObstacleDifficulityMin && ObstaclesValue <= ObstacleDifficulityMax;
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
        return _obstaclePools[type].GetItem();
    }
}
