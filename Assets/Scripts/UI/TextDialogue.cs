using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextDialogue: TextBoxParent
{
    private bool dialogueOn;
    private bool canSkip;
    private bool onLastLine;

    public int currentLevel;
    public int currentChat;
    public int currentSection;
    public int currentLine;

    private void Start()
    {
        GameManager.scriptDialogue = GetComponent<TextDialogue>();

    }
    public void Write(int level, int chat, int section, int line)
    {
        dialogueOn = true;

        if (GameManager.scriptComment.textBoxAnimator.GetBool("active"))
        {
            GameManager.scriptComment.StopAllCoroutines();
            GameManager.scriptComment.CloseTextBox();
        }
        if (GameManager.scriptLog.textBoxAnimator.GetBool("active"))
        {
            GameManager.scriptLog.StopAllCoroutines();
            GameManager.scriptLog.CloseTextBox();
        }

        currentLevel = level;
        currentChat = chat;
        currentSection = section;
        currentLine = line;

        canSkip = true;
        Write(LibraryDialogue.RetrieveDialogue(level, chat, section, line));

        if (LibraryDialogue.RetrieveDialogue(level, chat, section, line + 1) == null) onLastLine = true;
        else onLastLine = false;

    }

    public override void FinishedTypeWriting()
    {
        base.FinishedTypeWriting();
        canSkip = false;
    }

    private void Update()
    {
        if (dialogueOn == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (canSkip)
                {
                    StopAllCoroutines();
                    textBoxText.text = textToWrite;
                    FinishedTypeWriting();
                }
                else if (canEnd == true)
                {
                    if (onLastLine)
                    {
                        CloseTextBox();
                        dialogueOn = false;

                        if (GameManager.CutscenePlaying)
                        {
                            GameManager.currentCutscene.currentEvent += 1;
                            GameManager.currentCutscene.PlayEvent();
                        }
                        else GameManager.Cutscene(false);
                    }
                    else Write(currentLevel, currentChat, currentSection, currentLine + 1);

                }
            }
        }
        
    }

}
