using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMaskForItems;
    public Transform fuelContainer;

    public CharacterMovement characterMovement;

    private int fuelCollected;
    // Start is called before the first frame update
    void Start()
    {
    }

 

	private void OnTriggerEnter(Collider otherGameObject)
	{
        bool hasCollidedWithItem = (1 << otherGameObject.transform.gameObject.layer) == this.layerMaskForItems.value;
        if (hasCollidedWithItem)
		{
            var item = otherGameObject.GetComponent<Item>();

            bool isItemFuel = otherGameObject.gameObject.CompareTag(TAGS.FUEL_TAG);
            if (isItemFuel)
			{
                if (!item.isCollected)
                {
                    CollectFuel(otherGameObject.gameObject, item);
                }
            }

            bool isItemBoost = otherGameObject.gameObject.CompareTag(TAGS.BOOST_TAG);
            if (isItemBoost)
			{
                CollectBoost(otherGameObject.GetComponent<BoostItem>());
            }
        }

	}

    private void CollectBoost(BoostItem boost)
    {
        characterMovement.MovementSpeed = boost.boostedSpeed;
        boost.gameObject.SetActive(false);
        boost.isCollected = true;
        Invoke("ResetSpeed", boost.boostDuration);
    }

    private void ResetSpeed()
    {
        characterMovement.MovementSpeed = 6;
    }

    private void CollectFuel(GameObject otherGameObject, Item fuel)
	{
        otherGameObject.transform.parent = fuelContainer;
        otherGameObject.transform.localPosition = 1.1f * Vector3.up * fuelCollected;
        otherGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        fuelCollected += 1;
        fuel.isCollected = true;
    }
}
