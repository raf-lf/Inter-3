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
    static public KeyCode keyWalk = KeyCode.LeftShift;

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
    public Animator[] actionArmAnimator = new Animator[2];
    public Transform actionHand;

    [Header("Animation Test")]
    static public KeyCode keyTest = KeyCode.C;
    private bool inTestAnimation;

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

            actionArmAnimator[0].Play("actionArm_throw");

            StopAllCoroutines();
            StartCoroutine(RightActionArmReady(0));
            StartCoroutine(ActionArmEnd(throwAnimationEnd));

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

    IEnumerator EnableDisableActions(bool disable, float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.CantAct = disable;

    }

    IEnumerator RightActionArmReady(float delay)
    {
        yield return new WaitForSeconds(delay);
            Player.CantAct = true;
            GameManager.scriptWeapons.HideUnhideWeapons(true);
            GameManager.scriptWeapons.playerArms[0].enabled = false;
            GameManager.scriptWeapons.playerArms[1].enabled = true;

    }

    IEnumerator LeftActionArmReady(float delay)
    {
        yield return new WaitForSeconds(delay);
            Player.CantAct = true;
            GameManager.scriptWeapons.HideUnhideWeapons(true);
            GameManager.scriptWeapons.playerArms[0].enabled = true;
            GameManager.scriptWeapons.playerArms[1].enabled = false;

    }

    IEnumerator ActionArmEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
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

    public void CommsAnimation(int animationToPlay)
    {

        switch (animationToPlay)
        {
            case 1:
                StartCoroutine(LeftActionArmReady(0));
                actionArmAnimator[1].Play("commsOn");
                break;
            case 2:
                StartCoroutine(ActionArmEnd(1.3f));
                actionArmAnimator[1].Play("commsOff");
                break;
            case 3:
                StartCoroutine(LeftActionArmReady(0));
                actionArmAnimator[1].Play("commsCall");
                break;

        }

    }

    public void testAnimationUse()
    {
        if (Player.CantAct == false && inTestAnimation == false)
        {
            StartCoroutine(LeftActionArmReady(0));
            StartCoroutine(EnableDisableActions(false, 2));
            actionArmAnimator[1].Play("commsOn");
            inTestAnimation = true;
        }

        else if (Player.CantAct == false && inTestAnimation == true)
        {
            StartCoroutine(ActionArmEnd(1.3f));
            actionArmAnimator[1].Play("commsOff");
            inTestAnimation = false;
        }

    }

    private void Update()
    {
        if (GameManager.GamePaused == false)
        {
            if (Input.GetKeyDown(keyTest))
            {
                testAnimationUse();

            }
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
                if (Input.GetKeyDown(keyWeapon[i]) && GameManager.unlockedWeapon[i]) GameManager.scriptWeapons.SwapWeapon(i);
            }

            if (remainingPulses > 0 && Time.time > pulseTimer)
            {
                Pulse();
            }
        }
    }

}
