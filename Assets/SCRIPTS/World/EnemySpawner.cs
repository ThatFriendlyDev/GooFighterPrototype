using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : TimedSpawner
{
    [Header("Enemy Spawner Coordinates")]
    public float minimumDistanceFromPlayer;
    public float maximumDistanceFromPlayer;

    public GameObject playerGameObject;

	protected override void Spawn(Vector3 positionToSpawnAt = new Vector3())
	{
        var randomDistance = Random.Range(minimumDistanceFromPlayer, maximumDistanceFromPlayer);
        var randomDirection2D = Random.insideUnitCircle.normalized;
        var randomDirection3D = new Vector3(randomDirection2D.x, 0, randomDirection2D.y);
        var randomPosition = playerGameObject.transform.position + randomDistance * randomDirection3D;

        base.Spawn(randomPosition);
	}

}
