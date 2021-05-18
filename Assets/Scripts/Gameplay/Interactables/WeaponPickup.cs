using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactible
{
    public int weaponId;
    private void Start()
    {
        //Destroy this item if it was already picked in a previous save
        if (GameManager.unlockedWeapon[weaponId]) Destroy(gameObject);

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

        GameManager.unlockedWeapon[weaponId] = true;
        GameManager.scriptHud.UpdateWeaponUnlocks();

    }
}
