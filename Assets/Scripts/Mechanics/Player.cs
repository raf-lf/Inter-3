using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int hp = 10;
    public static int hpMax = 10;

    public int iFrames = 60;
    public int iFramesCount = -1;
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
        ChangeHp(hpLoss * -1);
        
        ToggleIFrames(true);

        if (knockback != 0)
            {
                GetComponent<PlayerMovement>().Knockback(knockback, sourcePosition);
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
    }

    private void ToggleIFrames(bool toggleOn)
    {
        if (toggleOn)
        {
            iFramesCount = iFrames;

            DamageCollider.enabled = false;

            if (inCover)
            {
                Cover(false);
            }

            shaderAnimator.SetBool("damaged", true);

        }
        else
        {
            DamageCollider.enabled = true;

            shaderAnimator.SetBool("damaged", false);
        }

    }

    public void Cover (bool covering)
    {
        inCover = covering;

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

    private void FixedUpdate()
    {
        if (iFramesCount > 0)
        {
            iFramesCount--;
        }
        else if (iFramesCount == 0)
        {
            ToggleIFrames(false);
            iFramesCount = -1;
        }
      
    }



}


