using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Animation : CutsceneEvent
{
    public Animator animator;
    [Header("Animation Method (1: Play / 2:SetBool / 3:SetInt)")]
    public int animationMethod;
    public string parameter;
    public bool boolValue;
    public int intValue;

    public override void ExecuteEvent()
    {

        switch(animationMethod)
        {
            case 1:
                animator.Play("parameter");
                break;
            case 2:
                animator.SetBool("parameter", boolValue);
                break;
            case 3:
                animator.SetInteger("parameter", intValue);
                break;

        }
        base.ExecuteEvent();

    }

}
