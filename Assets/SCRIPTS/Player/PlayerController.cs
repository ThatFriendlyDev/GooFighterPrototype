using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMaskForItems;
    public Transform fuelContainer;

    private int fuelCollected;
    // Start is called before the first frame update
    void Start()
    {
    }

   

	private void OnTriggerEnter(Collider other)
	{
        bool hasCollidedWithItem = 1 << other.transform.gameObject.layer == this.layerMaskForItems.value;
        if (hasCollidedWithItem)
		{
            other.transform.parent = fuelContainer;
            other.transform.localPosition = Vector3.up * fuelCollected;
            fuelCollected += 1;
           // other.gameObject.SetActive(false);
        }
	}
}
