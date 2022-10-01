using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    private EventData eventData;
    
    private void Awake()
    {
        Singleton();
        eventData = Resources.Load("EventData") as EventData;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
