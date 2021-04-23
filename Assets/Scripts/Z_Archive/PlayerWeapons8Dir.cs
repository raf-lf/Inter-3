using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons8Dir : MonoBehaviour
{
    public static bool[] unlockedWeapon = { true, true, true};
    public static int equipedWeapon = -1;
    public static int[] ammo = { 0, 6, 50 };
    public static int[] magazineSize = { 0, 6, 50 };
    public static int[] clips = { 0, 4, 2 };

    //Weapon can't fire when cooldown >0
    public int[] cooldownValues = { 60, 30, 10 };
    [SerializeField] private int[] cooldown = { 0, 0, 0 };

    //What effect/bullet a weapon spawns when it's used
    public GameObject[] effect = new GameObject[3];
    //Weapon objects
    public GameObject[] weapon = new GameObject[3];
    //Point of origin for effects/bullets in weapon objects
    public Transform[] effectOrigin = new Transform[3];

    [SerializeField] private int shootingDirection = 0;
    private float rotationZ;
    private Vector2 direction;

    public SpriteRenderer[] playerArms = new SpriteRenderer[2];
    public Animator weaponGroupAnimator;


    //Updates weapon hotkeys at start
    private void Start()
    {
        if (ManagerUi.ManagerUiScript == null)
        {
            ManagerUi.ManagerUiScript = GameObject.Find("UI").GetComponent<ManagerUi>();
        }

    UpdateWeaponUnlocks();    
    }

    //Swap Weapon method is accessed through PlayerActions class
    public void SwapWeapon(int weaponId)
    {
        if (Player.PlayerControls)
        {
            //First, reset arm sprites
            foreach (SpriteRenderer arm in playerArms)
            {
                arm.enabled = true;
            }

            //If key for equipped weapon is pressed, unequip weapon
            if (weaponId == equipedWeapon)
            {
                equipedWeapon = -1;

            }
            //Else equips weapon
            else
            {
                equipedWeapon = weaponId;

                cooldown[weaponId] = 30;

                //Since weapon id 2 is two-handed, makes both arms disappear
                if (weaponId == 2)
                {
                    foreach (SpriteRenderer arm in playerArms)
                    {
                        arm.enabled = false;
                    }
                }
                //Since other weapons are one-handed makes only the arm holding the weapon disappear
                else playerArms[0].enabled = false;
            }

            //Checks all weapons
            for (int i = 0; i < weapon.Length; i++)
            {
                //Does the following for the equiped weapon...
                if (i == equipedWeapon)
                {
                    //Activates weapon object sprites
                    SpriteRenderer[] weaponSprites;
                    weaponSprites = weapon[i].GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer sprite in weaponSprites)
                    {
                        sprite.enabled = true;

                    }

                    //Enables ammo bar, ignores if melee
                    if (i != 0) ManagerUi.ManagerUiScript.ammoBarAnimator[i].SetBool("hidden", false);
                    //Enables active hotkey icon
                    ManagerUi.ManagerUiScript.itemHotkeyAnimator[i].SetBool("active", true);
                }
                //Does the following for all other weapons
                else
                {
                    //Activates weapon object sprites
                    SpriteRenderer[] weaponSprites;
                    weaponSprites = weapon[i].GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer sprite in weaponSprites)
                    {
                        sprite.enabled = false;

                    }

                    if (i != 0) ManagerUi.ManagerUiScript.ammoBarAnimator[i].SetBool("hidden", true);
                    ManagerUi.ManagerUiScript.itemHotkeyAnimator[i].SetBool("active", false);

                }
            }
        }

    }

    //Reload method is accessed through PlayerActions class
    public void Reload()
    {
        //Can't reload melee, when clip is full or when out of clips
        if (equipedWeapon > 0 && ammo[equipedWeapon] < magazineSize[equipedWeapon] && clips[equipedWeapon] >0)
        {
            cooldown[equipedWeapon] = 30;
            clips[equipedWeapon]--;
            ammo[equipedWeapon] = magazineSize[equipedWeapon];
        }
    }

    public void UpdateWeaponUnlocks()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            //Gets all weapons and copies unlock bool to animation state
            ManagerUi.ManagerUiScript.itemHotkeyAnimator[i].SetBool("enabled", unlockedWeapon[i]);
        }
    }

    void Update()
    {
        if (shootingDirection != 0) weaponGroupAnimator.SetBool("facingLeft", PlayerMovement.facingLeft);

        //Disables player's input completely if their control's are disabled
        if (Player.PlayerControls)
        {
            //Can't aim or attack while in cover
            if (Player.scriptPlayer.inCover == false)
            {
                //Define shooting direction with arrow keys. Use two arrow keys for diagonal. When direction = 0, player is not considered shooting
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)) shootingDirection = 2;
                else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow)) shootingDirection = 4;
                else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)) shootingDirection = 6;
                else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)) shootingDirection = 8;
                else if (Input.GetKey(KeyCode.UpArrow)) shootingDirection = 1;
                else if (Input.GetKey(KeyCode.RightArrow)) shootingDirection = 3;
                else if (Input.GetKey(KeyCode.DownArrow)) shootingDirection = 5;
                else if (Input.GetKey(KeyCode.LeftArrow)) shootingDirection = 7;
                else shootingDirection = 0;

                //Shoot if weapon is equiped and if out of cover
                if (equipedWeapon != -1)
                {
                    weapon[equipedWeapon].GetComponent<Animator>().SetInteger("direction", shootingDirection);
                    weapon[equipedWeapon].GetComponent<Animator>().SetBool("facingLeft", PlayerMovement.facingLeft);
                    



                    //Melee
                    if (equipedWeapon == 0 && cooldown[equipedWeapon] == 0 && shootingDirection != 0)
                    {
                        cooldown[equipedWeapon] = cooldownValues[equipedWeapon];

                        GameObject createdSlash = Instantiate(effect[equipedWeapon]);
                        createdSlash.transform.position = effectOrigin[equipedWeapon].position;
                        WeaponMelee script = createdSlash.GetComponent<WeaponMelee>();
                        script.DirectionSpeed(shootingDirection);
                    }

                    //Other Weapons
                    else if (ammo[equipedWeapon] > 0 && cooldown[equipedWeapon] == 0 && shootingDirection != 0)
                    {
                        ammo[equipedWeapon]--;
                        cooldown[equipedWeapon] = cooldownValues[equipedWeapon];

                        GameObject attack = Instantiate(effect[equipedWeapon]);
                        attack.transform.position = effectOrigin[equipedWeapon].transform.position;

                        switch (shootingDirection)
                        {
                            case 1:
                                rotationZ = 90;
                                direction = new Vector2(0, 1);
                                break;
                            case 2:
                                rotationZ = 45;
                                direction = new Vector2(0.5f, 0.5f);
                                break;
                            case 3:
                                rotationZ = 0;
                                direction = new Vector2(1, 0);
                                break;
                            case 4:
                                rotationZ = -45;
                                direction = new Vector2(0.5f, -0.5f);
                                break;
                            case 5:
                                rotationZ = -90;
                                direction = new Vector2(0, -1);
                                break;
                            case 6:
                                rotationZ = -135;
                                direction = new Vector2(-0.5f, -0.5f);
                                break;
                            case 7:
                                rotationZ = -180;
                                direction = new Vector2(-1, 0);
                                break;
                            case 8:
                                rotationZ = -225;
                                direction = new Vector2(-0.5f, 0.5f);
                                break;
                        }

                        attack.GetComponent<Projectile>().rotationZ = rotationZ;
                        attack.GetComponent<Projectile>().direction = direction;

                        /*
                        GameObject createdShot = Instantiate(effect[equipedWeapon]);
                        createdShot.transform.position = effectOrigin[equipedWeapon].position;
                        WeaponBullet bulletScript = createdShot.GetComponent<WeaponBullet>();
                        bulletScript.DirectionSpeed(direction);
                        */
                    }
                }
            }

        }
    }

    //Update cooldown for all weapons
    private void FixedUpdate()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (cooldown[i] > 0) cooldown[i] -= 1;
        }


    }
}
