using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;


enum EventCode
{
    playerPosition,
    attack,
    spawn
}

public class EventManager : MonoBehaviour, IOnEventCallback
{
    public static EventManager eventManager;

    public void EventListener(EventData photonEvent)
    {
        ((IOnEventCallback)eventManager).EventListener(photonEvent);
    }

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
