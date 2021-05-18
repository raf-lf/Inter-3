using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ContinueDialogue : CutsceneEvent
{
    public override void ExecuteEvent()
    {

        int level = GameManager.scriptDialogue.currentLevel;
        int chat = GameManager.scriptDialogue.currentChat;
        int section = GameManager.scriptDialogue.currentSection;

        GameManager.scriptDialogue.Write(level, chat, section +1, 1);


    }
    
}
