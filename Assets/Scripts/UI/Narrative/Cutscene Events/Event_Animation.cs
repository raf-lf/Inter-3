using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Animation : CutsceneEvent
{
    public enum parameterType
    {
        boolean, integer, Float, playAnimation
    }
    public parameterType type;
    public string parameterName;

    public bool boolValue;
    public int intValue;
    public float floatValue;

    public Animator animator;

    public override void ExecuteEvent()
    {

        switch(type)
        {
            case parameterType.playAnimation:
                animator.Play(parameterName);
                break;
            case parameterType.boolean:
                animator.SetBool(parameterName, boolValue);
                break;
            case parameterType.integer:
                animator.SetInteger(parameterName, intValue);
                break;
            case parameterType.Float:
                animator.SetFloat(parameterName, floatValue);
                break;

        }

        base.ExecuteEvent();

    }

}
