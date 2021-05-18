using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_AnimatePlayer : CutsceneEvent
{
    public enum animationType
    {
        comCall, commReceive, commTurnoff, custom
    }
    public animationType type;

    public string customAnimation;


    public override void ExecuteEvent()
    {
        if (type == animationType.custom)
        {
            GameManager.PlayerCharacter.GetComponent<Animator>().Play(customAnimation);

        }
        else switch (type)
            {
                case animationType.comCall:
                    GameManager.scriptActions.CommsAnimation(3);
                    break;
                case animationType.commReceive:
                    GameManager.scriptActions.CommsAnimation(1);
                    break;
                case animationType.commTurnoff:
                    GameManager.scriptActions.CommsAnimation(2);
                    break;

            }

        base.ExecuteEvent();
    }

}
