using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	[Header("Pool Objects")]
	public Transform fuel;
	public Transform boost;

	static ResourceManager _instance;

	public static ResourceManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<ResourceManager>();
			}

			return _instance;
		}
	}
}
