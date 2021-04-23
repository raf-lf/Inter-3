using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int hp = 10;
    public static int hpMax = 10;

    public int iFrames = 1;
    private bool inIFrames;
    public SpriteRenderer[] spriteGroup;

    public static int itemHeal = 3;
    public static int itemGrenade = 3;

    public bool inCover;
    public Collider2D DamageCollider;
    public Collider2D CoverCollider;

    public static GameObject PlayerCharacter;
    public static bool PlayerControls = true;

    public static Player scriptPlayer;
    public static PlayerWeapons scriptWeapons;
    public static PlayerMovement scriptMovement;
    public static PlayerActions scriptActions;

    public Animator shaderAnimator;
    private bool dead;


    void Start()
    {
        PlayerCharacter = this.gameObject;
        scriptPlayer = GetComponent<Player>();
        scriptWeapons = GetComponent<PlayerWeapons>();
        scriptMovement = GetComponent<PlayerMovement>();
        scriptActions = GetComponent<PlayerActions>();
    }

    public void Damage(int hpLoss, float knockback, Transform sourcePosition)
    {
        if (inIFrames == false)
        {
            ChangeHp(hpLoss * -1);

            ToggleIFrames(true);

            if (knockback != 0 && sourcePosition != null)
            {
                GetComponent<PlayerMovement>().Knockback(knockback, sourcePosition);
            }
        }
    }

    public void Heal(int hpGain)
    {
            ChangeHp(hpGain);
    }

    public void ChangeHp(int change)
    {
        if (hp + change < 0) hp = 0;
        else if (hp + change > hpMax) hp = hpMax;
        else hp += change;

        if (hp == 0) Death();
    }
    
    public void Death()
    {
        dead = true;
        CoverCollider.enabled = false;
        DamageCollider.enabled = false;

        Animator anim = GetComponent<Animator>();
        anim.SetBool("death", true);
        Player.scriptWeapons.SwapWeapon(PlayerWeapons.equipedWeapon);
        PlayerControls = false;
        shaderAnimator.SetBool("dead", true);
        StartCoroutine(PostDeath());
    }

    IEnumerator PostDeath()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp = hpMax;
        PlayerControls = true;


    }

    private void ToggleIFrames(bool toggleOn)
    {
        inIFrames = toggleOn;

        DamageCollider.enabled = !toggleOn;
        shaderAnimator.SetBool("damaged", toggleOn);

        if (toggleOn)
        {
            if (inCover) Cover(false);
            StartCoroutine(IFramesCount());
        }

    }

    public void Cover (bool covering)
    {
        inCover = covering;

        CoverCollider.enabled = covering;
        DamageCollider.enabled = !covering;

        if (covering) GetComponent<PlayerMovement>().HaltMovement();

        Animator anim = GetComponent<Animator>();
        anim.SetBool("cover", covering);
        shaderAnimator.SetBool("cover", covering);

        spriteGroup = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spriteGroup)
        {
            if (covering) sprite.sortingLayerName = "Cover";
            else sprite.sortingLayerName = "Default";
        }


    }

    IEnumerator IFramesCount()
    {
        yield return new WaitForSeconds(iFrames);
        if (dead == false) ToggleIFrames(false);
    }


}


