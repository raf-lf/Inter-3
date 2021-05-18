using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutsceneEvent : MonoBehaviour
{
    public float nextEventInterval;
    public virtual void ExecuteEvent()
    {
        StartCoroutine(DelayEvent());
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(nextEventInterval);

        GetComponent<Cutscene>().currentEvent += 1;
        GetComponent<Cutscene>().PlayEvent();

    }
}
