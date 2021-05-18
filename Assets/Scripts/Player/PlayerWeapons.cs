using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Weapon Stats")]
    public static int equipedWeapon = -1;
    public int equipedWeaponMemory;
    public static int[] ammo = { 0, 6, 50 };
    public static int[] magazineSize = { 0, 6, 50 };

    //Weapon can't fire when cooldown >0
    public int[] cooldownValues = { 30, 30, 10 };
    [SerializeField] private int[] cooldown = { 0, 0, 0 };

    //What effect/bullet a weapon spawns when it's used
    public GameObject[] effect = new GameObject[3];
    public int unupgradedPistolClipSize = 6;
    public int upgradedPistolClipSize = 10;
    public float rifleCriticalChance = 0.15f;
    public GameObject rifleCriticalEffect;
    public GameObject pistolSecondaryShot;
    public GameObject rifleSecondaryShot;

    [Header("Weapon Objects")]
    public Animator meleeAnimator;
    //Weapon objects
    public GameObject[] weapon = new GameObject[3];
    //Point of origin for effects/bullets in weapon objects
    public Transform[] effectOrigin = new Transform[3];

    public float rotationZ;
    public Vector2 direction;

    public SpriteRenderer[] playerArms = new SpriteRenderer[2];


    //Swap Weapon method is accessed through PlayerActions class
    public void SwapWeapon(int weaponId)
    {
        if (Player.PlayerControls && Player.CantAct == false && GameManager.GamePaused == false)
        {
            //If key for equipped weapon is pressed, unequip weapon
            if (weaponId == equipedWeapon)
            {
                if (equipedWeapon != -1) GameManager.scriptPlayerAudio.SfxSwap();
                equipedWeapon = -1;
                playerArms[0].enabled = true;
                playerArms[1].enabled = true;
            }

            //Else equips weapon
            else
            {
                equipedWeapon = weaponId;
                if (equipedWeapon != -1)  GameManager.scriptPlayerAudio.SfxSwap();
                playerArms[0].enabled = false;
                if (equipedWeapon == 2) playerArms[1].enabled = false;
                else playerArms[1].enabled = true;
            }

            //hides all weapons
            HideUnhideWeapons(true);

            //unhides equipped weapon
            HideUnhideWeapons(false);

            //UI Hotkey animator control
            for (int i = 0; i < weapon.Length; i++)
            {
                //Does the following for the equiped weapon
                if (i == equipedWeapon)
                {
                    //Enables ammo bar, ignores if melee
                    if (i != 0) GameManager.scriptHud.ammoBarAnimator[i].SetBool("hidden", false);
                    //Enables active hotkey icon
                    GameManager.scriptHud.itemHotkeyAnimator[i].SetBool("active", true);
                }
                //Does the following for all other weapons
                else
                {
                    //Disables ammo bar, ignores if melee
                    if (i != 0) GameManager.scriptHud.ammoBarAnimator[i].SetBool("hidden", true);
                    //Disables active hotkey icon
                    GameManager.scriptHud.itemHotkeyAnimator[i].SetBool("active", false);
                }
            }
        }

    }


    public void HideUnhideWeapons(bool hide)
    {
        SpriteRenderer[] weaponSprites;

        if (hide)
        {
            foreach (GameObject weap in weapon)
            {
                weaponSprites = weap.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sprite in weaponSprites)
                {
                    sprite.enabled = false;
                }
            }
        }
        else if (equipedWeapon != -1)
        {
            weaponSprites = weapon[equipedWeapon].GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in weaponSprites)
            {
                sprite.enabled = true;
            }
        }
    }

    //Reload method is accessed through PlayerActions class
    public void Reload()
    {
        //Can't reload melee, when clip is full or when out of clips
        if (equipedWeapon > 0 && ammo[equipedWeapon] < magazineSize[equipedWeapon] && GameManager.AmmoClips[equipedWeapon] >0)
        {
            cooldown[equipedWeapon] = 30;
            GameManager.AmmoClips[equipedWeapon]--;
            ammo[equipedWeapon] = magazineSize[equipedWeapon];
            GameManager.scriptPlayerAudio.SfxReload();
        }
    }

  
    public void UpdateAim()
    { 
        //Gets mouse position
        Vector3 lookTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        
        Vector2 lookDirection;

        if(equipedWeapon == -1) lookDirection = lookTarget - transform.position;
        else lookDirection = lookTarget - weapon[equipedWeapon].transform.position;

        rotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        //Gets rotation based on difference
        //Gets direction for projectiles
        float distance = lookDirection.magnitude;
        direction = lookDirection / distance;
        direction.Normalize();
    }

    public void UpdateWeaponRotation()
    {
        //rotates weapon
        if (rotationZ > 90 || rotationZ < -90) weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 180, -rotationZ + 180);
        else weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 0, rotationZ);

    }
    public void UpdateMeleeWeaponRotation()
    {
        //rotates weapon
        if (PlayerMovement.facingLeft) weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 180, 0);
        else weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    public void MeleeAttackEnd()
    {
        Player.CantAct = false;
        UpdateMeleeWeaponRotation();
    }


    public void AttackMelee()
    {
        if (cooldown[equipedWeapon] == 0)
        {
            UpdateWeaponRotation();
            Player.CantAct = true;

            cooldown[equipedWeapon] = cooldownValues[equipedWeapon];
            meleeAnimator.Play("melee_attack");

            Invoke("MeleeAttackEnd", 0.5f);
        }
    }

    
    public void Attack()
    {
        if (cooldown[equipedWeapon] == 0)
        {
            if (ammo[equipedWeapon] > 0)
            {
                ammo[equipedWeapon]--;
                cooldown[equipedWeapon] = cooldownValues[equipedWeapon];


                GameObject projectileToSpawn = effect[equipedWeapon];

                if (equipedWeapon == 2 && GameManager.weaponUpgrades[2])
                {
                    float roll = Random.Range(1, 101);
                    if (roll <= rifleCriticalChance) projectileToSpawn = rifleCriticalEffect;

                }

                GameObject attack = Instantiate(projectileToSpawn, effectOrigin[equipedWeapon].transform.position, Quaternion.Euler(0, 0, rotationZ));
                attack.transform.position = effectOrigin[equipedWeapon].transform.position;

                //Configures projectile
                attack.GetComponent<Projectile>().direction = direction;
            }
            else
            {
                GameManager.scriptPlayerAudio.SfxNoAmmo();
                cooldown[equipedWeapon] = cooldownValues[equipedWeapon];
            }
        }

    }
    

    void Update()
    {
        if (GameManager.weaponUpgrades[0] && magazineSize[1] != upgradedPistolClipSize) magazineSize[1] = upgradedPistolClipSize;

        if (GameManager.GamePaused == false)
        {
            UpdateAim();

            //Disables player's input completely if their control's are disabled
            if (Player.PlayerControls && Player.CantAct == false)
            {
                //Can't aim or attack while in cover
                if (GameManager.scriptPlayer.inCover == false && equipedWeapon != -1)
                {
                    /*
                    weapon[equipedWeapon].GetComponent<Animator>().SetInteger("direction", shootingDirection);
                    weapon[equipedWeapon].GetComponent<Animator>().SetBool("facingLeft", PlayerMovement.facingLeft);
                    */

                    if (equipedWeapon != 0) UpdateWeaponRotation();

                    if (Input.GetMouseButton(0))
                    {
                        switch (equipedWeapon)
                        {
                            case 0:
                                AttackMelee();
                                break;

                            default:
                                Attack();
                                break;
                        }
                    }
                    else if (Input.GetMouseButton(1) && cooldown[equipedWeapon] <= 0)
                    {

                        switch (equipedWeapon)
                        {
                            case 0:
                                AttackMelee();
                                break;

                            default:
                                {
                                    break;
                                }
                        }
                    }


                }

            }
        }
    }

    //Update cooldown for all weapons
    private void FixedUpdate()
    {
        if (GameManager.GamePaused == false)
        {
            for (int i = 0; i < weapon.Length; i++)
            {
                if (cooldown[i] > 0) cooldown[i] -= 1;
            }
        }


    }
}
