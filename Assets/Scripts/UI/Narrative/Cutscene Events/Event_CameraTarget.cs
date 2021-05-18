using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_CameraTarget : CutsceneEvent
{
    public float lerpSpeed = 0.05f;
    public Transform cameraTarget;

    public override void ExecuteEvent()
    {
        GameManager.scriptCamera.followSpeed = lerpSpeed;

        GameManager.scriptCamera.ChangeTarget(cameraTarget);

        base.ExecuteEvent();

    }

}
