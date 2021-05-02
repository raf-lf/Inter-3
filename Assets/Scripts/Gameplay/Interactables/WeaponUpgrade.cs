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

        GameManager.scriptLog.Write(LibraryMenu.LogUpgrade(upgradeId));

    }
}
