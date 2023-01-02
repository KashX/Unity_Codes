using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInst : MonoBehaviour
{
    [SerializeField]
    private GameObject butterfly;

    [SerializeField]
    private Transform pos;
    
    private bool once;    

    private void OnTriggerEnter(Collider col)
    {

        if(col.tag == "Player" && !once)
        {     
            Instantiate(butterfly, pos.transform.position, pos.transform.rotation);
            once = true;
        }
    }

}