using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutsceneEvent : MonoBehaviour
{

    public virtual void ExecuteEvent()
    {
        GetComponent<Cutscene>().currentEvent += 1;
        GetComponent<Cutscene>().PlayEvent();

    }
}
