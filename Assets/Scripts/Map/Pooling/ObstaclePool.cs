
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclePool
{
    private Obstacle _obstacle;
    private ObjectPool<Obstacle> _pool;
    private UpdatedObstacleManager _manager;

    public ObstaclePool(Obstacle obstacle, UpdatedObstacleManager manager)
    {
        _manager = manager;
        _obstacle = obstacle;
        _pool = new ObjectPool<Obstacle>(OnPoolCreateObstacle, OnPoolTakeObstacle, OnPoolReturnObstacle, OnPoolDestroybstacle, true, 30, 100);
        WarmUpPool();
    }
    public Obstacle GetObstacle()
    {
        return _pool.Get();
    }
    public void ReturnObstacle(Obstacle obstacle)
    {
        _pool.Release(obstacle);
    }

    private void WarmUpPool()
    {
        List<Obstacle> objects = new(10);

        for (int i = 0; i < objects.Count; i++)
        {
            objects.Add(_pool.Get());
        }

        for (int i = 0; i < objects.Count; i++)
        {
            _pool.Release(objects[i]);
        }
    }
    private Obstacle OnPoolCreateObstacle()
    {
        Obstacle obstacle = GameObject.Instantiate(_obstacle);
        obstacle.Manager = _manager;
        obstacle.gameObject.SetActive(false);
        return obstacle;
    }
    private void OnPoolTakeObstacle(Obstacle obstacle)
    {
        
    }
    private void OnPoolReturnObstacle(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
    }
    private void OnPoolDestroybstacle(Obstacle obstacle)
    {
        GameObject.Destroy(obstacle);
    }
    
}
