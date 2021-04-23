using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCrate : Interactible
{
    [Header("Treasure")]
    public int pistolClips;
    public int rifleClips;
    public int grenade;
    public int heal;

    public override void Interact()
    {
        Debug.Log("Supply crate " + gameObject.name + " used.");
        GetComponent<Animator>().SetBool("open", true);

        PlayerWeapons.clips[1] += pistolClips;
        PlayerWeapons.clips[2] += rifleClips;
        Player.itemGrenade += grenade;
        Player.itemHeal += heal;
    }
}
