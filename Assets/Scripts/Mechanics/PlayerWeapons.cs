using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
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

    private float rotationZ;
    private Vector2 direction;

    public SpriteRenderer[] playerArms = new SpriteRenderer[2];


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
  
    public void UpdateWeaponRotation()
    {
        
        //Gets mouse position
        Vector3 lookTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        Vector2 lookDirection = lookTarget - weapon[equipedWeapon].transform.position;
        rotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        //Gets rotation based on difference
        //Gets direction for projectiles
        float distance = lookDirection.magnitude;
        direction = lookDirection / distance;
        direction.Normalize();

        //rotates weapon
        if(rotationZ > 90 || rotationZ < -90) weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 180, -rotationZ + 180);
        else weapon[equipedWeapon].transform.rotation = Quaternion.Euler(0, 0, rotationZ);

    }

    public void AttackMelee()
    {
        if (cooldown[equipedWeapon] == 0)
        {
            cooldown[equipedWeapon] = cooldownValues[equipedWeapon];

            GameObject attack = Instantiate(effect[equipedWeapon], effectOrigin[equipedWeapon].transform.position, Quaternion.Euler(0, 0, rotationZ));
            attack.transform.position = effectOrigin[equipedWeapon].position;

            WeaponMelee script = attack.GetComponent<WeaponMelee>();
            //script.DirectionSpeed(shootingDirection);
        }

    }
    public void Attack()
    { 
        if (ammo[equipedWeapon] > 0 && cooldown[equipedWeapon] == 0)
        {
            ammo[equipedWeapon]--;
            cooldown[equipedWeapon] = cooldownValues[equipedWeapon];

            GameObject attack = Instantiate(effect[equipedWeapon], effectOrigin[equipedWeapon].transform.position, Quaternion.Euler(0,0,rotationZ));
            attack.transform.position = effectOrigin[equipedWeapon].transform.position;
            
            //Configures projectile
            attack.GetComponent<Projectile>().direction = direction;
        }

    }

    void Update()
    {
        //Disables player's input completely if their control's are disabled
        if (Player.PlayerControls)
        {
            //Can't aim or attack while in cover
            if (Player.scriptPlayer.inCover == false && equipedWeapon != -1)
            {
                /*
                weapon[equipedWeapon].GetComponent<Animator>().SetInteger("direction", shootingDirection);
                weapon[equipedWeapon].GetComponent<Animator>().SetBool("facingLeft", PlayerMovement.facingLeft);
                */

                UpdateWeaponRotation();

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
