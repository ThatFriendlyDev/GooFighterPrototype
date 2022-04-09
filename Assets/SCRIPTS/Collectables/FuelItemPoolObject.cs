using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItemPoolObject : PoolObject
{
	[SerializeField]
	private FuelItem fuelItem;

	public override void OnObjectReuse()
	{
		//fuelItem.isCollected = false;
	}
}
