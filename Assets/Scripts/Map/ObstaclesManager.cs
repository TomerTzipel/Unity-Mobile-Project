using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int obstacleSpawnRate;
    [SerializeField] private int obstacleHeight;
    [SerializeField] private int obstacleThickness;
    private int _obstacleSpawnCounter = 0;

    private ObjectPool<GameObject> _obstaclesPool;

    private GameObject LastTile
    {
        get { return gameManager.LastTile; }
    }

    void Awake()
    {
        _obstaclesPool = new ObjectPool<GameObject>(PoolCreateObstacle, OnPoolTakeObstacle, OnPoolReturnObstacle, OnPoolDestroybstacle, true, 80, 160);
        WarmUpPool();
    }

    private void WarmUpPool()
    {
        List<GameObject> objects = new(20);

        for (int i = 0; i < 20; i++)
        {
            objects.Add(_obstaclesPool.Get());
        }

        for (int i = 0; i < 20; i++)
        {
            _obstaclesPool.Release(objects[i]);
        }
    }

    private GameObject PoolCreateObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab);

        obstacle.SetActive(false);
        obstacle.transform.localScale = new Vector3(gameManager.FirstLastTile.transform.localScale.x / 3, obstacleHeight, obstacleThickness);

        return obstacle;
    }
    private void OnPoolTakeObstacle(GameObject obstacle)
    {
    }
    private void OnPoolReturnObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
    }
    private void OnPoolDestroybstacle(GameObject obstacle)
    {
        Destroy(obstacle);
    }

    public void ReturnObstacleToPool(GameObject obstacle)
    {
        _obstaclesPool.Release(obstacle);
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
        float obstacleZ = LastTile.transform.position.z;

        float lastTileX = LastTile.transform.position.x;
        float surfaceY = LastTile.transform.position.y + (LastTile.transform.localScale.y / 2);
        float lastTileWidth = LastTile.transform.localScale.x;

        float middleObstacleX = lastTileX;
        float rightObstacleX = lastTileX + lastTileWidth / 3;
        float leftObstacleX = lastTileX - lastTileWidth / 3;


        List<ObstacleType> types = new() { ObstacleType.Open, ObstacleType.FullyBlocked, ObstacleType.JumpOnly, ObstacleType.SlideOnly, ObstacleType.SlideJumpOnly, ObstacleType.NotJump, ObstacleType.AnyJump };

        ObstacleType middleObstacleType = ChooseObstacleType(types);
        ObstacleType rightObstacleType = ChooseObstacleType(types);
        ObstacleType leftObstacleType = ChooseObstacleType(types);

        SpawnObstacle(middleObstacleType, middleObstacleX, surfaceY, obstacleZ);
        SpawnObstacle(rightObstacleType, rightObstacleX, surfaceY, obstacleZ);
        SpawnObstacle(leftObstacleType, leftObstacleX, surfaceY, obstacleZ);
    }

    private ObstacleType ChooseObstacleType(List<ObstacleType> types)
    {
        int chosenTypeIndex = Random.Range(0, types.Count);
        ObstacleType chosenType = types[chosenTypeIndex];
        types.RemoveAt(chosenTypeIndex);

        return chosenType;
    }

    private void SpawnObstacle(ObstacleType type, float x, float surfaceY, float z)
    {
        switch (type)
        {
            case ObstacleType.FullyBlocked:
                SpawnFullyBlockedObstacle(x, surfaceY, z);
                break;
            case ObstacleType.JumpOnly:
                SpawnJumpOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.SlideOnly:
                SpawnCrouchOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.SlideJumpOnly:
                SpawnCrouchJumpOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.NotJump:
                SpawnNotJumpObstacle(x, surfaceY, z);
                break;
            case ObstacleType.AnyJump:
                SpawnNotCrouchObstacle(x, surfaceY, z);
                break;
        }
    }

    private void SpawnFullyBlockedObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleMiddle = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + heightHalf, z);
        obstacleMiddle.transform.position = new Vector3(x, surfaceY + (heightHalf * 3), z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 5), z);

        obstacleLower.transform.parent = LastTile.transform;
        obstacleMiddle.transform.parent = LastTile.transform;
        obstacleUpper.transform.parent = LastTile.transform;

        obstacleLower.SetActive(true);
        obstacleMiddle.SetActive(true);
        obstacleUpper.SetActive(true);

    }
    private void SpawnJumpOnlyObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + heightHalf, z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 6), z);

        obstacleLower.transform.parent = LastTile.transform;
        obstacleUpper.transform.parent = LastTile.transform;

        obstacleLower.SetActive(true);
        obstacleUpper.SetActive(true);
    }
    private void SpawnCrouchOnlyObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + (heightHalf * 2), z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 4), z);

        obstacleLower.transform.parent = LastTile.transform;
        obstacleUpper.transform.parent = LastTile.transform;

        obstacleLower.SetActive(true);
        obstacleUpper.SetActive(true);
    }
    private void SpawnCrouchJumpOnlyObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + heightHalf, z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 3), z);

        obstacleLower.transform.parent = LastTile.transform;
        obstacleUpper.transform.parent = LastTile.transform;

        obstacleLower.SetActive(true);
        obstacleUpper.SetActive(true);
    }
    private void SpawnNotJumpObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacle = _obstaclesPool.Get();

        float heightHalf = obstacle.transform.localScale.y / 2;

        obstacle.transform.position = new Vector3(x, surfaceY + (heightHalf * 2), z);

        obstacle.transform.parent = LastTile.transform;

        obstacle.SetActive(true);
    }
    private void SpawnNotCrouchObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacle = _obstaclesPool.Get();

        float heightHalf = obstacle.transform.localScale.y / 2;

        obstacle.transform.position = new Vector3(x, surfaceY + heightHalf, z);

        obstacle.transform.parent = LastTile.transform;

        obstacle.SetActive(true);
    }
}
