using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLogic : MonoBehaviour
{
    static MatchLogic _instance;

    public int _macPoints = 5;
    public TextAlignment _pointsText;
    public GameObject _finishUI;
    private int _points = 0;

    void Start()
    {
        _instance = this;
    }

    void UpdatePointsText()
    {
        
    }

    public static void AddPoint()
    {
        Debug.Log("Added 1 to Score");
    }
}
