using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyPrefab : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
   
    void Start()
    {
        SelfDestructWithParent();
    }
    public void SelfDestruct()
    {
        
        Destroy(gameObject);
    }

    public void SelfDestructWithParent()
    {
        
        Destroy(gameObject, waitTime);
    }
}
