using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : DamageContact
{

    protected override void DamagePlayer(Player script)
    {
        base.DamagePlayer(script);
        GetComponentInChildren<PlayAudio>().playSFX();
    }
}
