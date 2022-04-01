using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float enemySpawnCooldown;
    public float minimumDistanceFromPlayer;
    public float maximumDistanceFromPlayer;

    private float lastEnemySpawnedTimestamp;

    public GameObject playerGameObject;

    void Update()
    {
        if (Time.time > lastEnemySpawnedTimestamp + enemySpawnCooldown)
        {
            lastEnemySpawnedTimestamp = Time.time;
            var randomDistance = Random.Range(minimumDistanceFromPlayer, maximumDistanceFromPlayer);
            var randomDirection2D = Random.insideUnitCircle.normalized;
            var randomDirection3D = new Vector3(randomDirection2D.x, 0, randomDirection2D.y);
            var randomPosition = playerGameObject.transform.position + randomDistance * randomDirection3D;
            PoolManager.instance.reuseObject(ResourceManager.instance.enemy.gameObject, randomPosition, Quaternion.identity, Vector3.one);
        }
    }
}
