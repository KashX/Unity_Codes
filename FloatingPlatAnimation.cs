using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> floatingrocks = new List<GameObject>();
    [SerializeField] private List<Transform> targetPosition = new List<Transform>();
    [SerializeField] private List<Vector3> targetRotation = new List<Vector3>();
    [SerializeField] private float duration;

    void Start()
    {
        for(int i = 0; i < floatingrocks.Count; i++)
        {   
            LeanTween.move(floatingrocks[i], targetPosition[i], duration);
            LeanTween.rotate(floatingrocks[i], targetRotation[i], duration);
        }
    }
}
