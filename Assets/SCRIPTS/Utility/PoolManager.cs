using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public Pool[] _pools;

	Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

	static PoolManager _instance;

	public static PoolManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<PoolManager>();
			}

			return _instance;
		}
	}

	private void Awake()
	{
		for (int i = 0; i < this._pools.Length; i++)
		{
			this.createPool(this._pools[i]._poolObject, this._pools[i]._poolSize);
		}
	}

	public void createPool(GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID();

		GameObject poolHolder = new GameObject(prefab.name + " pool");
		if (!poolDictionary.ContainsKey(poolKey))
		{
			poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
			for (int i = 0; i < poolSize; i++)
			{
				ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
				newObject.GetTransform().gameObject.SetActive(false);
				poolDictionary[poolKey].Enqueue(newObject);
				newObject.SetParent(poolHolder.transform);

			}
		}
	}

	public ObjectInstance reuseObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		int poolKey = prefab.GetInstanceID();
		if (poolDictionary.ContainsKey(poolKey))
		{
			ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
			poolDictionary[poolKey].Enqueue(objectToReuse);

			objectToReuse.Reuse(position, rotation, scale);
			return objectToReuse;
		}

		return null;
	}

 

	[System.Serializable]
	public struct Pool
	{
		public GameObject _poolObject;
		public int _poolSize;
	}

	public class ObjectInstance
	{
		GameObject poolGameObject;
		Transform poolObjectTransform;

		bool isPoolObject;
		PoolObject poolObjectScript;

		public ObjectInstance(GameObject objectInstance)
		{
			poolGameObject = objectInstance;
			poolObjectTransform = poolGameObject.transform;
			poolObjectScript = poolGameObject.GetComponent<PoolObject>();
			poolObjectScript.Initialize();
			//poolObjectScript.OnObjectDelete();
			poolGameObject.SetActive(false);
			if (poolObjectScript != null)
			{
				this.isPoolObject = true;

			}
		}

		public void Reuse(Vector3 position, Quaternion rotation, Vector3 scale)
		{

			if (this.isPoolObject && poolObjectTransform)
			{
				poolObjectTransform.position = position;
				poolObjectTransform.rotation = rotation;
				poolObjectTransform.localScale = scale;

				poolGameObject.SetActive(true);

				poolObjectScript.OnObjectReuse();
			}
		}

		public void SetParent(Transform parent)
		{
			poolObjectTransform.parent = parent;
		}

		public void Destroy()
		{
			poolGameObject.SetActive(false);
			poolObjectScript.OnObjectDelete();
		}

		public Transform GetTransform()
		{
			return poolObjectTransform;
		}

 
	}
}
