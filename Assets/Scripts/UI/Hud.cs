using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public Image hpBar;
    public Text hpText;

    public Image[] ammoBar = new Image[3];
    public Text[] ammoText = new Text[3];
    public Text[] clipText = new Text[3];
    public Animator[] ammoBarAnimator = new Animator[3];
    public Animator[] itemHotkeyAnimator = new Animator[3];

    private float hpBarFill;
    private float v1;
    private float v2;
    private float[] ammoBarFill = new float[3];

    public Text itemHeals;
    public Text itemGrenades;


    private void Start()
    {
        GameManager.scriptHud = GetComponent<Hud>();

    }

    void Update()
    {
        hpText.text = Player.hp + "/" + Player.hpMax;
        v1 = Player.hp;
        v2 = Player.hpMax;
        hpBarFill = v1 / v2;
        hpBar.fillAmount = hpBarFill;

        itemHeals.text = "" + GameManager.ItemHeal;
        itemGrenades.text = "" + GameManager.ItemGrenade;


        //Repeat for every weapon
        for (int i = 0; i < ammoBar.Length; i++)
        {
            //Ignore for weapon 0 (melee)
            if (i != 0)
            {
                ammoText[i].text = "" + PlayerWeapons.ammo[i] + "/"+PlayerWeapons.magazineSize[i];
                clipText[i].text = "" + GameManager.AmmoClips[i];
                v1 = PlayerWeapons.ammo[i];
                v2 = PlayerWeapons.magazineSize[i];
                ammoBarFill[i] = v1 / v2;
                ammoBar[i].fillAmount = ammoBarFill[i];
            }
        }
    }
}
