using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public enum ObstacleType
{
    Open,FullyBlocked,JumpOnly,CrouchOnly,CrouchJumpOnly,NotJump,NotCrouch
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private GameObject firstLastTile;

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int obstacleSpawnRate;
    [SerializeField] private int obstacleHeight;
    [SerializeField] private int obstacleThickness;
    private int _obstacleSpawnCounter = 0;

    private ObjectPool<GameObject> _obstaclesPool;

    private void Awake()
    {
        SO_MapSettings.LastTile = firstLastTile;

        _obstaclesPool = new ObjectPool<GameObject>(PoolCreateObstacle, OnPoolTakeObstacle, OnPoolReturnObstacle, OnPoolDestroybstacle,true,80,160);
    }

    private GameObject PoolCreateObstacle()
    {
        GameObject obstacle = Instantiate (obstaclePrefab);

        obstacle.SetActive(false);
        obstacle.transform.localScale = new Vector3(firstLastTile.transform.localScale.x/3, obstacleHeight, obstacleThickness);

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



    private GameObject LastTile
    {
        get { return SO_MapSettings.LastTile; }
    }

    public void CheckObstaclesSpawn()
    {
        _obstacleSpawnCounter++;
        if (_obstacleSpawnCounter == obstacleSpawnRate)
        {
            SpawnObstacles();
        }
    }

    public void SpawnObstacles()
    {
        _obstacleSpawnCounter = 0;

        float obstacleZ = LastTile.transform.position.z;
     
        float lastTileX = LastTile.transform.position.x;
        float surfaceY = LastTile.transform.position.y + (LastTile.transform.localScale.y/2);
        float lastTileWidth = LastTile.transform.localScale.x;

        float middleObstacleX = lastTileX;
        float rightObstacleX = lastTileX + lastTileWidth / 3;
        float leftObstacleX = lastTileX - lastTileWidth / 3;

        List<ObstacleType> types = new List<ObstacleType>() { ObstacleType.Open, ObstacleType.FullyBlocked, ObstacleType.JumpOnly, ObstacleType.CrouchOnly, ObstacleType.CrouchJumpOnly, ObstacleType.NotJump, ObstacleType.NotCrouch };

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

    private void SpawnObstacle(ObstacleType type,float x,float surfaceY, float z)
    {
        switch (type)
        {
            case ObstacleType.FullyBlocked:
                SpawnFullyBlockedObstacle(x, surfaceY, z);
                break;
            case ObstacleType.JumpOnly:
                SpawnJumpOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.CrouchOnly:
                SpawnCrouchOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.CrouchJumpOnly:
                SpawnCrouchJumpOnlyObstacle(x, surfaceY, z);
                break;
            case ObstacleType.NotJump:
                SpawnNotJumpObstacle(x, surfaceY, z);
                break;
            case ObstacleType.NotCrouch:
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

        obstacleLower.gameObject.transform.parent = LastTile.transform;
        obstacleMiddle.gameObject.transform.parent = LastTile.transform;
        obstacleUpper.gameObject.transform.parent = LastTile.transform;

        obstacleLower.gameObject.SetActive(true);
        obstacleMiddle.gameObject.SetActive(true);
        obstacleUpper.gameObject.SetActive(true);

    }
    private void SpawnJumpOnlyObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + heightHalf, z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 6), z);

        obstacleLower.gameObject.transform.parent = LastTile.transform;
        obstacleUpper.gameObject.transform.parent = LastTile.transform;

        obstacleLower.gameObject.SetActive(true);
        obstacleUpper.gameObject.SetActive(true);
    }
    private void SpawnCrouchOnlyObstacle(float x, float surfaceY, float z)
    {
 
        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + (heightHalf * 2), z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 4), z);

        obstacleLower.gameObject.transform.parent = LastTile.transform;
        obstacleUpper.gameObject.transform.parent = LastTile.transform;

        obstacleLower.gameObject.SetActive(true);
        obstacleUpper.gameObject.SetActive(true);
    }
    private void SpawnCrouchJumpOnlyObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacleLower = _obstaclesPool.Get();
        GameObject obstacleUpper = _obstaclesPool.Get();

        float heightHalf = obstacleLower.transform.localScale.y / 2;

        obstacleLower.transform.position = new Vector3(x, surfaceY + heightHalf, z);
        obstacleUpper.transform.position = new Vector3(x, surfaceY + (heightHalf * 3), z);

        obstacleLower.gameObject.transform.parent = LastTile.transform;
        obstacleUpper.gameObject.transform.parent = LastTile.transform;

        obstacleLower.gameObject.SetActive(true);
        obstacleUpper.gameObject.SetActive(true);
    }
    private void SpawnNotJumpObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacle = _obstaclesPool.Get();

        float heightHalf = obstacle.transform.localScale.y / 2;

        obstacle.transform.position = new Vector3(x, surfaceY + (heightHalf * 2), z);

        obstacle.gameObject.transform.parent = LastTile.transform;

        obstacle.gameObject.SetActive(true);
    }
    private void SpawnNotCrouchObstacle(float x, float surfaceY, float z)
    {

        GameObject obstacle = _obstaclesPool.Get();

        float heightHalf = obstacle.transform.localScale.y / 2;

        obstacle.transform.position = new Vector3(x, surfaceY + heightHalf, z);

        obstacle.gameObject.transform.parent = LastTile.transform;

        obstacle.gameObject.SetActive(true);
    }





}
