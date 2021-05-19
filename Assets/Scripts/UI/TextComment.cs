using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextComment: TextBoxParent
{
    public float timeDelayPerCharacter = 0.1f;
    public float additionDelayTime = 2;
    private float endTimer;
    private bool onLastLine;

    private int currentLevel;
    private int currentSection;
    private int currentLine;

    private void Start()
    {
        GameManager.scriptComment = GetComponent<TextComment>();

    }
    public void Write(int level, int section, int line)
    {
        endTimer = 0;
        currentLevel = level;
        currentSection = section;
        currentLine = line;

        Write(LibraryDialogue.RetrieveComment(level, section, line));

        if (LibraryDialogue.RetrieveComment(level, section, line + 1) == null) onLastLine = true;
        else onLastLine = false;

    }

    public override void FinishedTypeWriting()
    {
        base.FinishedTypeWriting();
        endTimer = Time.time + additionDelayTime + (textToWrite.Length * timeDelayPerCharacter);
    }

    private void Update()
    {
        if (Time.time > endTimer && canEnd)
        {
            canEnd = false;
            if (onLastLine) CloseTextBox();
            else Write(currentLevel, currentSection, currentLine + 1);
        }
        
    }

}
