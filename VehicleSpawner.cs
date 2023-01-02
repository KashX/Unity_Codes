using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform pos1;
    [SerializeField]
    private Transform pos2;
    [SerializeField]
    private GameObject vehicleObject;

    [SerializeField]
    private float interval;
    private float timer;

   

    void Start()
    {
        
    }

    private void Update()
    {
        float randomInterval = Random.Range(2.0f, 3.0f);

        timer += Time.deltaTime;
        if (timer >= randomInterval)
        {
            SpawnVehicles();
            timer -= randomInterval;
        }
    }

    public void SpawnAtRightLane()
    {
        Instantiate(vehicleObject, pos1.position, pos1.rotation);
        Debug.Log("Spawned At Right");
    }

    public void SpawnAtLeftLane()
    {
        Instantiate(vehicleObject, pos2.position, pos2.rotation);
        Debug.Log("Spawned AT Left");
    }

    public void SpawnVehicles()
    {
        float randomChance = Random.Range(0.0f, 1.0f);
        if(randomChance < 0.4f)
        {
            SpawnAtLeftLane();
        }
        else
        {
            SpawnAtRightLane();
        }
    }
}