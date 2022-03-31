using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public float fuelSpawnCooldown;
    private float lastFuelSpawnedTimestamp;

    void Update()
    {
        if(Time.time > lastFuelSpawnedTimestamp + fuelSpawnCooldown)
		{
            lastFuelSpawnedTimestamp = Time.time;
            var x = Random.Range(-20, 20);
            var z = Random.Range(-20, 20);
            var position = new Vector3(x, 1, z);
            PoolManager.instance.reuseObject(WorldInitializer.instance.fuel.gameObject, position, Quaternion.identity, Vector3.one);
		}
    }
}
