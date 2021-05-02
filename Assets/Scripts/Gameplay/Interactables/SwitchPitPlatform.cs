using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPitPlatform : Interactible
{
    public bool isActive;
    public PitPlatform[] platformsToMove = new PitPlatform[1];

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().SetBool("active", !GetComponent<Animator>().GetBool("active"));
        isActive = !isActive;

        for (int i =0; i < platformsToMove.Length;  i++)
        {
            platformsToMove[i].changedPosition = !platformsToMove[i].changedPosition;

        }

    }

}
