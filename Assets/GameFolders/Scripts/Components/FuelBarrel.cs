using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarrel : MonoBehaviour
{
    [SerializeField] private float fuelAmount;
    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    public float GetFuelAmount()
    {
        // Animation process
        return fuelAmount;
    }
}
