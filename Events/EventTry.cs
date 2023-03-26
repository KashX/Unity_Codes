using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ActionTry.OnGameOver += Check;
    }

    // Update is called once per frame
    void Check()
    {
        Debug.Log("Checking");
    }
}
