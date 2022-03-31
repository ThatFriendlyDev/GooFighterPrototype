using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInitializer : MonoBehaviour
{
    public Transform fuel;

	static WorldInitializer _instance;

	public static WorldInitializer instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<WorldInitializer>();
			}

			return _instance;
		}
	}

}
