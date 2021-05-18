using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscavationPitSwitch : Interactible
{
    public Turret turret;
    public Animator[] animatorsToAffect;
    public Cutscene cutscene;

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().Play("switch_once");

        turret.active = !turret.active;
        turret.animator.SetBool("inactive", !turret.animator.GetBool("inactive"));

        for(int i = 0; i < animatorsToAffect.Length; i++)
        {
            animatorsToAffect[i].SetBool("active", !animatorsToAffect[i].GetBool("active"));
        }


        if (turret.active)
        {
            turret.attackCooldownFrames = turret.attackCooldown;
            turret.audioOn.playSFX();
        }
        else
        {

            turret.audioOff.playSFX();
        }

        if (cutscene.off == false)
        {
            cutscene.CutsceneStartEnd(true);

        }

    }

}
