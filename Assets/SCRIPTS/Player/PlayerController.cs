using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask layerMaskForInteractable;
    public LayerMask layerMaskForItems;
    public LayerMask layerMaskForEnemies;
    [Space(20)]

    [Header("Enemies Detection Settings")]
    public float enemyDetectRadius;
    public float enemyDetectCooldown;
    private float lastEnemyDetectionTimestamp;
    private Collider[] enemiesWithinRadiusList;
    private Transform nearestEnemy;
    

    public Transform fuelContainer;
    private List<Transform> collectedFuelItems;

    [Header("Player Components")]
    public CharacterMovement characterMovement;
    public CharacterOrientation3D characterOrientation;
    public Animator charaterAnimator;

	private void Awake()
	{
        collectedFuelItems = new List<Transform>();
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

	private void Update()
	{
        bool shouldCheckForEnemies = Time.time > enemyDetectCooldown + lastEnemyDetectionTimestamp;
        if (shouldCheckForEnemies)
		{
            lastEnemyDetectionTimestamp = Time.time;
            enemiesWithinRadiusList = Physics.OverlapSphere(transform.position, enemyDetectRadius, layerMaskForEnemies.value);
            
            if (enemiesWithinRadiusList.Length > 0)
			{
                nearestEnemy = enemiesWithinRadiusList[0].transform;
                var distanceToNearestEnemy = Vector3.Distance(transform.position, nearestEnemy.position);

                for (int i = 0; i < enemiesWithinRadiusList.Length; i++)
                {
                    var distanceToNextEnemy = Vector3.Distance(transform.position, enemiesWithinRadiusList[i].transform.position);
                    if (distanceToNextEnemy < distanceToNearestEnemy)
					{
                        nearestEnemy = enemiesWithinRadiusList[i].transform;
                        distanceToNearestEnemy = distanceToNextEnemy;
                    }
                }
            } else
			{
                nearestEnemy = null;
			}
        }

        if (nearestEnemy)
		{
            characterOrientation.RotationMode = CharacterOrientation3D.RotationModes.WeaponDirection;
            charaterAnimator.SetBool("IsShootingParam", true);
            // transform.rotation = Quaternion.LookRotation((nearestEnemy.position - transform.position).normalized);
        }
		else
        {
            charaterAnimator.SetBool("IsShootingParam", false);
            characterOrientation.RotationMode = CharacterOrientation3D.RotationModes.MovementDirection;
        }
       
        
	}


	private void OnTriggerEnter(Collider otherGameObject)
	{
        Debug.Log(otherGameObject.gameObject.name);
        bool hasCollidedWithItem = (1 << otherGameObject.transform.gameObject.layer) == this.layerMaskForItems.value;
        if (hasCollidedWithItem)
		{
            var item = otherGameObject.GetComponent<PickableItem>();

            

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
        //otherGameObject.transform.parent = fuelContainer;
        // otherGameObject.transform.localPosition = 0.6f * Vector3.up * collectedFuelItems.Count;
        //otherGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        //collectedFuelItems.Add(otherGameObject.transform);
    }

    public void CollectFuel(FuelItem fuel)
    {
        fuel.OnCollected(fuelContainer, 0.6f * Vector3.up * collectedFuelItems.Count);
        collectedFuelItems.Add(fuel.transform);
    }

 
}
