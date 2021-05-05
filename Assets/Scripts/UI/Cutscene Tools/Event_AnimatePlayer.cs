using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_AnimatePlayer : CutsceneEvent
{
    public string animationName;

    [Header("Play Comm Aniamtions: 1 - Up, 2 - Down, - 3 Call")]
    public int playCommAnimation;

    public override void ExecuteEvent()
    {
        if (playCommAnimation == 0)
        {
            GameManager.PlayerCharacter.GetComponent<Animator>().Play(animationName);
        }
        else GameManager.scriptActions.CommsAnimation(playCommAnimation);

        base.ExecuteEvent();
    }

}
