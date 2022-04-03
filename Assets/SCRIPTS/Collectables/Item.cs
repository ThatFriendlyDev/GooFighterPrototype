using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnCollected() {
        isCollected = true;
    }

    public virtual void OnCollected(Transform newParent, Vector3 positionToMoveTo) {
        isCollected = true;
    }
}
