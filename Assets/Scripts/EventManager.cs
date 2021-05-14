using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventManager : MonoBehaviour
{
    public static EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        if(eventManager == null)
        {
            eventManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
