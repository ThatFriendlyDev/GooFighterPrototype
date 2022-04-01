using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask layerMaskForInteractable;
    public LayerMask layerMaskForItems;
    [Space(20)]
    public Transform fuelContainer;
    private List<Transform> collectedFuelItems;

    public CharacterMovement characterMovement;

	private void Awake()
	{
        collectedFuelItems = new List<Transform>();
    }

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


        bool hasCollidedWithInteractable = (1 << otherGameObject.transform.gameObject.layer) == this.layerMaskForInteractable.value;
        if (hasCollidedWithInteractable)
		{
            bool isDropZone = otherGameObject.gameObject.CompareTag(TAGS.DROP_ZONE);
            if (isDropZone)
			{

                otherGameObject.GetComponent<DropZoneCtrl>().AddFuelItems(collectedFuelItems);
                collectedFuelItems.Clear();
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
        otherGameObject.transform.localPosition = 0.6f * Vector3.up * collectedFuelItems.Count;
        otherGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        fuel.isCollected = true;
        collectedFuelItems.Add(otherGameObject.transform);
    }
     
}
