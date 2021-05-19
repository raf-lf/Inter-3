using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene: MonoBehaviour
{
    public bool off;
    public bool triggerColliderActivation = true;

    [Header("Events")]
    public int currentEvent = 0;
    public CutsceneEvent[] events = new CutsceneEvent[1];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && off == false && triggerColliderActivation)
        {
            CutsceneStartEnd(true);

        }
        
    }

    public void CutsceneStartEnd(bool start)
    {
        if (start)
        {
            off = true;
            currentEvent = 0;
            GameManager.Cutscene(true);
            PlayEvent();
        }
        else GameManager.Cutscene(false);

    }

    public void PlayEvent()
    {
        if (currentEvent < events.Length)
        {
            Debug.Log("Played event "+ currentEvent);
            events[currentEvent].ExecuteEvent();
        }
        else
        {
            Debug.Log("Cutscene ended.");

            CutsceneStartEnd(false);


        }

    }
}

