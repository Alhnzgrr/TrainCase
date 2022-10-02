using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarrel : MonoBehaviour
{
    [SerializeField] private float fuelAmount;

    public float GetFuelAmount()
    {
        // Animation process
        return fuelAmount;
    }
}
