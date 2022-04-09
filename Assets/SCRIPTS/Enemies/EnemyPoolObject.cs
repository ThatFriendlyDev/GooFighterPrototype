using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolObject : PoolObject
{
    private Character character;
    // Start is called before the first frame update
    void Start()
    {
 
    }


	public override void OnObjectReuse()
	{
		base.OnObjectReuse();
        GetComponent<Character>().ConditionState.ChangeState(CharacterStates.CharacterConditions.Normal);
    }
}
