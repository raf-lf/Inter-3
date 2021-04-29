using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Hotkeys")]
    static public KeyCode keyJump = KeyCode.Space;

    static public KeyCode keyInteract = KeyCode.E;

    static public KeyCode keyHeal = KeyCode.Q;
    static public KeyCode keyGrenade = KeyCode.F;

    static public KeyCode keyReload = KeyCode.R;
    static public KeyCode keyCover = KeyCode.W;
    static public KeyCode keyCrouch = KeyCode.S;

    static public KeyCode[] keyWeapon = {KeyCode.Alpha1, KeyCode.Alpha2 , KeyCode.Alpha3};

    [Header("Heal")]
    public int hpHeal = 1;
    public int healPulses = 10;
    public float hpPulseInterval = 1f;
    public float remainingPulses = 0;
    private float pulseTimer;
    public GameObject healEffect;
    private float healCooldown = 1f;
    private float healCooldownTimer;


    [Header("Grenade")]
    public GameObject grenadeObject;
    public float throwSpeed;
    private float throwRotationSpeed = 1000;
    private float throwAnimationGrenadeDelay = .1f;
    private float throwAnimationEnd = .5f;
    private float throwCooldown = .5f;
    private float throwCooldownTimer;
    public Animator actionAnimator;
    public Transform actionHand;


    private void ItemHealUse()
    {
        if (GameManager.ItemHeal > 0 && Player.PlayerControls && Player.CantAct == false && Time.time >= healCooldownTimer && remainingPulses == 0)
        {
            GameManager.ItemHeal -= 1;

            HealActivate(true);
            remainingPulses = healPulses;

            healCooldownTimer = Time.time + healCooldown;
        }
    }

    public void HealActivate(bool activate)
    {
        healEffect.GetComponent<Animator>().SetBool("active", activate);

    }

    public void Pulse()
    {
        pulseTimer = Time.time + hpPulseInterval;
        GameManager.scriptPlayer.Heal(hpHeal);
        healEffect.GetComponent<Animator>().Play("Healing_pulse");
        remainingPulses -= 1;

        if (remainingPulses == 0) HealActivate(false);
    }

    private void ItemGrenadelUse()
    {
        if (GameManager.ItemGrenade > 0 && GameManager.scriptPlayer.inCover == false && Player.PlayerControls && Player.CantAct == false && Time.time >= throwCooldownTimer)
        {
            GameManager.ItemGrenade -= 1;

            actionAnimator.Play("actionArm_throw");

            StopAllCoroutines();
            StartCoroutine(ActionAnimations(true, 0));
            StartCoroutine(ActionAnimations(false, throwAnimationEnd));

            throwCooldownTimer = Time.time + throwCooldown;

            Invoke("GrenadeThrow", throwAnimationGrenadeDelay);
        }
    }

    public void GrenadeThrow()
    {
        //Spawns in grenade
        GameObject grenade = Instantiate(grenadeObject);
        //Positions grenade in hand
        grenade.transform.position = actionHand.transform.position;
        //Makes grenade move according to current aim
        grenade.GetComponent<Rigidbody2D>().velocity += GameManager.scriptWeapons.direction * throwSpeed;
        //Make grenade spin according to facing direction
        Mathf.Abs(throwRotationSpeed);
        if (PlayerMovement.facingLeft) throwRotationSpeed *= -1;
        grenade.GetComponent<Rigidbody2D>().angularVelocity += throwRotationSpeed;   
    }

    IEnumerator ActionAnimations(bool enter, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enter)
        {
            Player.CantAct = true;
            GameManager.scriptWeapons.HideUnhideWeapons(true);
            GameManager.scriptWeapons.playerArms[0].enabled = false;
            GameManager.scriptWeapons.playerArms[1].enabled = true;
        }
        else
        {
            Player.CantAct = false;
            GameManager.scriptWeapons.HideUnhideWeapons(false);
            
            switch (PlayerWeapons.equipedWeapon)
            {
                case -1:
                    GameManager.scriptWeapons.playerArms[0].enabled = true;
                    GameManager.scriptWeapons.playerArms[1].enabled = true;
                    break;
                case 2:
                    GameManager.scriptWeapons.playerArms[0].enabled = false;
                    GameManager.scriptWeapons.playerArms[1].enabled = false;
                    break;
                default:
                    GameManager.scriptWeapons.playerArms[0].enabled = false;
                    GameManager.scriptWeapons.playerArms[1].enabled = true;
                    break;
            }

        }

    }

    private void Update()
    {
        if (GameManager.GamePaused == false)
        {
            if (Input.GetKeyDown(keyHeal))
            {
                ItemHealUse();
            }

            if (Input.GetKeyDown(keyGrenade))
            {
                ItemGrenadelUse();
            }

            if (Input.GetKeyDown(keyReload))
            {
                GameManager.scriptWeapons.Reload();
            }

            for (int i = 0; i < keyWeapon.Length; i++)
            {
                if (Input.GetKeyDown(keyWeapon[i]) && PlayerWeapons.unlockedWeapon[i]) GameManager.scriptWeapons.SwapWeapon(i);
            }

            if (remainingPulses > 0 && Time.time > pulseTimer)
            {
                Pulse();
            }
        }
    }

}
