using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class EnemyController : MonoBehaviour
{

    public AIBrain aiBrain;
    public float spawnDuration;
    public Health healthComponent;
    // Start is called before the first frame update
    void Start()
    {
        healthComponent = transform.GetComponent<Health>();
        healthComponent.OnDeath += OnEnemyDeath;
    }

	private void OnEnable()
	{
        aiBrain.enabled = false;
        Invoke("ActivateBrain", spawnDuration);
    }

    public void OnEnemyDeath()
	{

	}

	private void ActivateBrain()
	{
        aiBrain.enabled = true;
	}
}
