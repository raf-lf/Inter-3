using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_CameraReset : CutsceneEvent
{
    
    public override void ExecuteEvent()
    {
        GameManager.scriptCamera.ResetLerpSpeed();
        GameManager.scriptCamera.ResetTarget();
        GameManager.scriptCamera.ResetOffset();

        base.ExecuteEvent();

    }

}
