using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextLog: TextBoxParent
{
    public float timeDelayPerCharacter = 0.01f;
    public float additionDelayTime = 2;
    private float endTimer;
    private bool timerOn;

    private void Start()
    {
        endTimer = 0;
        GameManager.scriptLog = GetComponent<TextLog>();

    }
    public override void Write(string text)
    {

        textBoxAnimator.SetBool("active", true);
        textBoxText.text = text;

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
            CloseTextBox();
        }
        
    }

}
