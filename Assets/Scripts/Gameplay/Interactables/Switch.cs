﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactible
{
    public bool isActive;

    private void Start()
    {

    }

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().SetBool("active", !GetComponent<Animator>().GetBool("active"));
        isActive = !isActive;

    }

}
