using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Interactible
{
    public int upgradeId;

    public override void Interact()
    {
        GetComponent<Animator>().SetBool("active", false);

        GameManager.weaponUpgrades[upgradeId] = true;

        WriteText.scriptWrite.LogWrite(LibraryLog.LogUpgrade(upgradeId));

    }
}
