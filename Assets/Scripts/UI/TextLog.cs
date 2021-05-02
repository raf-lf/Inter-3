using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextLog: TextBoxParent
{
    public float timeDelayPerCharacter = 0.1f;
    public float additionDelayTime = 2;
    private float endTimer;
    private bool timerOn;

    private void Start()
    {
        GameManager.scriptLog = GetComponent<TextLog>();

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
