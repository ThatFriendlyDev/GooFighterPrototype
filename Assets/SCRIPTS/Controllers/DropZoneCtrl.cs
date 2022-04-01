using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropZoneCtrl : MonoBehaviour
{
    public float consumeDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Transform> fuelItemsToConsume;
    public void AddFuelItems(List<Transform> fuelItems)
	{
        fuelItemsToConsume = new List<Transform>();
		for (int i = 0; i < fuelItems.Count; i++)
		{
            fuelItemsToConsume.Add(fuelItems[i]);
        }
        StartCoroutine(ConsumeFuelItemsCoroutine(fuelItemsToConsume));
    }

    private IEnumerator ConsumeFuelItemsCoroutine(List<Transform> fuelItems)
	{
        
        float startTimestamp = Time.time;  
        float timeSinceStarted = Time.time - startTimestamp;
        float percentageComplete = 0;

        
        for (int i = 0; i < fuelItems.Count; i++)
        {
            fuelItems[i].transform.parent = transform;
            fuelItems[i].transform.localPosition = new Vector3(0, 0, -1.5f) + i * Vector3.up * 0.6f;
            fuelItems[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        float lastItemHeight = fuelItems.Last().localPosition.y;

        List<Vector3> initialPositionsOfItems = fuelItems.Select(p => p.localPosition).ToList();


        while (percentageComplete < 1)
		{
            timeSinceStarted = Time.time - startTimestamp;
            percentageComplete = timeSinceStarted / consumeDuration;
            for (int i = 0; i < fuelItems.Count; i++)
            {
                fuelItems[i].transform.localPosition = Vector3.Lerp(initialPositionsOfItems[i], initialPositionsOfItems[i] - lastItemHeight * Vector3.up, percentageComplete);
            }

            yield return null;
        }

        for (int i = 0; i < fuelItems.Count; i++)
        {
            fuelItems[i].gameObject.SetActive(false);
        }
    }
}
