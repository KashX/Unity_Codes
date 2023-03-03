using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    [SerializeField]
    private bool openTrigger = false;
    [SerializeField]
    private bool closeTrigger = false;
    [SerializeField]
    private bool winTrigger = false;

    [SerializeField]
    private string winscreen = "WinScreen"; 
    

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        { 
            if(openTrigger)
            {
                LeanTween.rotateY(door, -90f, 0.7f);
                
                // SoundManager.FindObjectOfType<SoundManager>().Play("GateOpenClose");
                // gameObject.SetActive(false);
            }
            else if(closeTrigger)
            {
                LeanTween.rotateY(door, 0f, 0.7f);

                // SoundManager.FindObjectOfType<SoundManager>().Play("GateOpenClose");
                // gameObject.SetActive(false);
            }
            else if(winTrigger)
            {
                
            }
        }
    }
}