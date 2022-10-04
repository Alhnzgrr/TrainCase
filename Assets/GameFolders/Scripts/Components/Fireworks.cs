using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    ParticleSystem[] fireworks;
    EventData _eventData;


    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;

        fireworks = GetComponentsInChildren<ParticleSystem>();
    }
    private void OnEnable()
    {
        _eventData.OnExplodeFireworks += ExplodeFireworks;
    }
    private void OnDisable()
    {
        _eventData.OnExplodeFireworks -= ExplodeFireworks;
    }

    private void ExplodeFireworks()
    {
        foreach (ParticleSystem firework in fireworks)
        {
            firework.Play();
        }
    }
}
