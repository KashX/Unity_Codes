using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField] private Transform playerT;
    [SerializeField] private Transform enemyT;
    [SerializeField] private GameObject indicatorNorth;
    [SerializeField] private GameObject indicatorSouth;
    [SerializeField] private GameObject indicatorEast;
    [SerializeField] private GameObject indicatorWest;


    private float dotProductN = 0;
    private float dotProductS = 0;
    private float dotProductE = 0;
    private float dotProductW = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = -Mathf.Infinity;
        Vector3 dir = enemyT.position - playerT.position;

        

        float distanceFromPlayer = Vector3.Distance(playerT.position, enemyT.position);

        dotProductN = Vector3.Dot(dir, Vector3.forward);
        dotProductS = Vector3.Dot(dir, Vector3.back);
        dotProductE = Vector3.Dot(dir, Vector3.right);
        dotProductW = Vector3.Dot(dir, Vector3.left);
        
        if(distanceFromPlayer > 3f && distanceFromPlayer < 8f)
        {
            if(dotProductN > closestDistance)
            {
                closestDistance = dotProductN;
                indicatorNorth.SetActive(true);
            }
            else
                indicatorNorth.SetActive(false);

            if(dotProductS > closestDistance)
            {
                closestDistance = dotProductS;
                indicatorSouth.SetActive(true);
            }
            else
                indicatorSouth.SetActive(false);

            if(dotProductE > closestDistance)
            {
                closestDistance = dotProductE;
                indicatorEast.SetActive(true);
            }
            else
                indicatorEast.SetActive(false);

            if(dotProductW > closestDistance)
            {
                closestDistance = dotProductW;
                indicatorWest.SetActive(true);
            }
            else
            indicatorWest.SetActive(false);
        }
        else
        {
            indicatorNorth.SetActive(false);
            indicatorSouth.SetActive(false);
            indicatorEast.SetActive(false);
            indicatorWest.SetActive(false);
        }
    }
}
