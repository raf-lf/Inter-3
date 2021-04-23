using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    static public KeyCode keyJump = KeyCode.Space;

    static public KeyCode keyInteract = KeyCode.E;

    static public KeyCode keyHeal = KeyCode.Q;
    static public KeyCode keyGrenade = KeyCode.F;

    static public KeyCode keyReload = KeyCode.R;
    static public KeyCode keyCover = KeyCode.W;
    static public KeyCode keyCrouch = KeyCode.S;

    static public KeyCode[] keyWeapon = {KeyCode.Alpha1, KeyCode.Alpha2 , KeyCode.Alpha3};



    private void ItemHealUse()
    {
        if (Player.itemHeal > 0 && Player.hp < Player.hpMax && Player.PlayerControls)
        {
            GetComponent<Player>().Heal(5);
            Player.itemHeal -= 1;
        }
    }

    private void ItemGrenadelUse()
    {
        if (Player.itemGrenade > 0 && Player.scriptPlayer.inCover == false && Player.PlayerControls)
        {
            Player.itemGrenade -= 1;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyHeal))
        {
            ItemHealUse();
        }

        if (Input.GetKeyDown(keyGrenade))
        {
            ItemGrenadelUse();
        }

        if (Input.GetKeyDown(keyReload))
        {
            Player.scriptWeapons.Reload();
        }

        for (int i=0; i < keyWeapon.Length;i++)
        {
            if (Input.GetKeyDown(keyWeapon[i]) && PlayerWeapons.unlockedWeapon[i]) Player.scriptWeapons.SwapWeapon(i);
        }

    }
}
