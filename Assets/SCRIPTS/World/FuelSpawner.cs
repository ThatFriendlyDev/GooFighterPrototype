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
            var x = Random.Range(-10, 10);
            var z = Random.Range(-10, 10);
            var position = new Vector3(x, 1, z);
            PoolManager.instance.reuseObject(ResourceManager.instance.fuel.gameObject, position, Quaternion.identity, 0.5f * Vector3.one);
		}
    }
}
