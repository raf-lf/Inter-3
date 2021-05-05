using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_DelayEvent : CutsceneEvent
{
    public float delaySeconds;
    public CutsceneEvent eventToDelay;

    public override void ExecuteEvent()
    {

        Invoke("DelayedExecution", delaySeconds);

    }
    
    private void DelayedExecution()
    {
        if (eventToDelay!= null) eventToDelay.ExecuteEvent();

        base.ExecuteEvent();

    }

}
