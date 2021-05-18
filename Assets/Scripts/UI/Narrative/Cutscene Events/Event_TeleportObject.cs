using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_TeleportObject : CutsceneEvent
{
    public enum objectType
    {
        player, otherObject
    }
    public objectType objectToTeleport;

    public GameObject otherObject;
    public Transform pointToTeleport;

    public override void ExecuteEvent()
    {
        GameObject teleported;

        if (objectToTeleport == objectType.otherObject) teleported = otherObject;
        else teleported = GameManager.PlayerCharacter;
        
        teleported.transform.position = pointToTeleport.position;
        
        base.ExecuteEvent();

    }
    
}
