using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_CallDialogue : CutsceneEvent
{
    [Header("Dialogue IDs")]
    public int level;
    public int chat;

    public override void ExecuteEvent()
    {
        GameManager.currentCutscene = GetComponent<Cutscene>();
        GameManager.scriptDialogue.Write(level, chat, 1, 1);


    }
    
}
