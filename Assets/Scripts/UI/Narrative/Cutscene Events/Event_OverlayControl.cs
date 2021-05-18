using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_OverlayControl : CutsceneEvent
{
    public enum control
    {
        fadeOut, fadeIn
    }
    public control controlType;

    public float animatorSpeed = 1;

    public override void ExecuteEvent()
    {
        Animator overlayAnimator = GameManager.overlay.GetComponent<Animator>();

        overlayAnimator.speed = animatorSpeed;

        switch (controlType)
        {
            case control.fadeIn:
                overlayAnimator.SetInteger("state", 0);
                break;
            case control.fadeOut:
                overlayAnimator.SetInteger("state", 1);
                break;
        }

        overlayAnimator.speed = 1;

        base.ExecuteEvent();

    }
    
}
