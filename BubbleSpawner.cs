using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;

    [SerializeField]
    private Transform pos;

    private void SpawnBubble()
    {     
        Instantiate(bubble, pos.transform.position, pos.transform.rotation);
    }
}