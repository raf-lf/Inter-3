using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Interactible
{
    public int upgradeId;

    private void Start()
    {
        //Destroy this item if it was already picked in a previous save
        if (GameManager.weaponUpgrades[upgradeId]) Destroy(gameObject);

    }

    public override void RememberLoad()
    {
        base.RememberLoad();
        Destroy(gameObject);

    }

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().SetBool("active", false);

        GameManager.weaponUpgrades[upgradeId] = true;

        GameManager.scriptLog.Write(LibraryMenu.LogUpgrade(upgradeId));

    }
}
