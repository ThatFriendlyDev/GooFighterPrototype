using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class EnemyController : MonoBehaviour
{

    public AIBrain aiBrain;
    public float spawnDuration;
    // Start is called before the first frame update
    void Start()
    {
       
    }

	private void OnEnable()
	{
        aiBrain.enabled = false;
        Invoke("ActivateBrain", spawnDuration);
    }

	private void ActivateBrain()
	{
        aiBrain.enabled = true;
	}
}
