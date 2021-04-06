using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManagerUi : MonoBehaviour
{
    public static ManagerUi ManagerUiScript;
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
        ManagerUiScript = GetComponent<ManagerUi>();

    }

    void Update()
    {
        hpText.text = Player.hp + "/" + Player.hpMax;
        v1 = Player.hp;
        v2 = Player.hpMax;
        hpBarFill = v1 / v2;
        hpBar.fillAmount = hpBarFill;

        itemHeals.text = "" + Player.itemHeal;
        itemGrenades.text = "" + Player.itemGrenade;


        //Repeat for every weapon
        for (int i = 0; i < ammoBar.Length; i++)
        {
            //Ignore for weapon 0 (melee)
            if (i != 0)
            {
                ammoText[i].text = "" + PlayerWeapons.ammo[i];
                clipText[i].text = "" + PlayerWeapons.clips[i];
                v1 = PlayerWeapons.ammo[i];
                v2 = PlayerWeapons.magazineSize[i];
                ammoBarFill[i] = v1 / v2;
                ammoBar[i].fillAmount = ammoBarFill[i];
            }
        }
    }
}
