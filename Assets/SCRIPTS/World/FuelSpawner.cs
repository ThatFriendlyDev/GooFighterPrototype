using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : TimedSpawner
{
	protected override void Spawn(Vector3 positionToSpawnAt = default)
	{
		var x = Random.Range(-10, 10);
		var z = Random.Range(-10, 10);
		var position = new Vector3(x, 0.2f, z);
		base.Spawn(position);
	}

}
