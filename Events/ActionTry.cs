using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionTry : MonoBehaviour
{
    public static event Action OnGameOver;
    
    public void Start()
    {
        OnGameOver?.Invoke();
    }
}
