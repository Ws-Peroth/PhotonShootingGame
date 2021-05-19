using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;


public enum EventCode : byte
{
    spawn = 1,
    playerPosition = 2,
    attack = 3,
}

public class EventManager : MonoBehaviour, IOnEventCallback
{
    public static EventManager eventManager;

    public void EventListener(EventData photonEvent)
    {
        ((IOnEventCallback)eventManager).EventListener(photonEvent);

    }

    public byte EventCodeToInt(EventCode eventCode)
    {
        if (eventCode == EventCode.spawn) return 1;
        else if (eventCode == EventCode.playerPosition) return 2;
        else if (eventCode == EventCode.attack) return 3;
        else return 0;
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
