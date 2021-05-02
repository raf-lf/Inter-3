using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextDialogue: TextBoxParent
{
    private bool canSkip;
    private bool onLastLine;

    private int currentLevel;
    private int currentSection;
    private int currentLine;

    private void Start()
    {
        GameManager.scriptDialogue = GetComponent<TextDialogue>();

    }
    public void Write(int level, int section, int line)
    {
        Player.PlayerControls = false;
        GameManager.scriptMovement.HaltMovement();

        currentLevel = level;
        currentSection = section;
        currentLine = line;

        canSkip = true;
        Write(LibraryDialogue.RetrieveDialogue(level, section, line));

        if (LibraryDialogue.RetrieveDialogue(level, section, line + 1) == null) onLastLine = true;
        else onLastLine = false;

    }

    public override void FinishedTypeWriting()
    {
        base.FinishedTypeWriting();
        canSkip = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

                    Player.PlayerControls = true;
                }
                else Write(currentLevel, currentSection, currentLine + 1);

            }
        }
        
    }

}
