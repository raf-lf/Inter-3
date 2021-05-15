using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMoveablePlatform : Interactible
{
    public MoveablePlatform[] platformsToMove = new MoveablePlatform[1];

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().Play("switch_once");

        for (int i =0; i < platformsToMove.Length;  i++)
        {
            platformsToMove[i].changedPosition = !platformsToMove[i].changedPosition;

        }

    }

}
