using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItem : PickableItem
{
    public float pickUpDuration;
 

	protected override void Pick(GameObject picker)
	{
        var playerController = picker.gameObject.MMGetComponentNoAlloc<PlayerController>();
        if (playerController)
		{
            playerController.CollectFuel(this);
        }
    }

	public void OnCollected(Transform fuelContainer, Vector3 positionToMoveTo)
    {
        StartCoroutine(StartMovingToPosition(fuelContainer, positionToMoveTo));
    }

    private IEnumerator StartMovingToPosition(Transform newParent, Vector3 positionToMoveTo)
    {
        transform.parent = newParent;

        float startTimestamp = Time.time;
        float timeSinceStarted = Time.time - startTimestamp;
        float percentageComplete = 0;
        float duration = pickUpDuration;

        var startPosition = transform.localPosition;
        var targetPosition = positionToMoveTo;

        var startRotation = transform.localRotation;
        var targetRotation = Quaternion.Euler(Vector3.zero);

        while (percentageComplete < 1)
        {
            timeSinceStarted = Time.time - startTimestamp;
            percentageComplete = timeSinceStarted / duration;

            transform.localPosition = Vector3.Slerp(startPosition, targetPosition, percentageComplete);
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, percentageComplete);
            yield return null;
        } 
        
    }
}
