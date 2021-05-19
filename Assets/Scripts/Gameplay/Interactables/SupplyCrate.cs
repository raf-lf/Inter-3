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
        base.Interact();
        
        GetComponent<Animator>().SetBool("open", true);

        int differentItemNumber=0;
        if (pistolClips > 0) differentItemNumber++;
        if (rifleClips > 0) differentItemNumber++;
        if (grenade > 0) differentItemNumber++;
        if (heal > 0) differentItemNumber++;


        if (differentItemNumber> 1) GameManager.scriptLog.Write(LibraryMenu.LogMultiple());
        else if (pistolClips > 0) GameManager.scriptLog.Write(LibraryMenu.LogAmmo(1, pistolClips));
        else if (rifleClips > 0) GameManager.scriptLog.Write(LibraryMenu.LogAmmo(2, rifleClips));
        else if (grenade > 0) GameManager.scriptLog.Write(LibraryMenu.LogGrenade(grenade));
        else if (heal > 0) GameManager.scriptLog.Write(LibraryMenu.LogHeal(heal));

        GameManager.AmmoClips[1] += pistolClips;
        GameManager.AmmoClips[2] += rifleClips;
        GameManager.ItemGrenade += grenade;
        GameManager.ItemHeal += heal;
    }
}
