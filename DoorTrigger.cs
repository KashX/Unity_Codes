using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject butterfly;

    [SerializeField]
    private Transform pos;
    
    private bool once;    
    
    [SerializeField]
    private Animator animator;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Trigger")
        { 
            animator.SetBool("trig", true);
        }

        if(col.tag == "Player" && !once)
        {     
            Instantiate(butterfly, pos.transform.position, pos.transform.rotation);
            once = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Trigger")
        { 
            animator.SetBool("trig", false);
        }
       
    }
}