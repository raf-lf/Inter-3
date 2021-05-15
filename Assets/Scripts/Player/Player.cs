using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int hp = 10;
    public static int hpMax = 10;

    public int iFramesSeconds = 1;
    private bool inIFrames;
    public SpriteRenderer[] spriteGroup;

    public bool inCover;
    public Collider2D DamageCollider;
    public Collider2D CoverCollider;

    public static bool PlayerControls = true;
    public static bool CantAct = false;

    public Animator shaderAnimator;
    private bool dead;


    void Start()
    {
        GameManager.PlayerCharacter = this.gameObject;
        GameManager.scriptPlayer = GetComponent<Player>();
        GameManager.scriptWeapons = GetComponent<PlayerWeapons>();
        GameManager.scriptMovement = GetComponent<PlayerMovement>();
        GameManager.scriptActions = GetComponent<PlayerActions>();
        GameManager.scriptPlayerAudio = GetComponent<PlayerAudio>();
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
        anim.Play("death");
        GameManager.scriptWeapons.SwapWeapon(PlayerWeapons.equipedWeapon);
        PlayerControls = false;
        shaderAnimator.SetBool("dead", true);

        if(GameManager.scriptActions.remainingPulses > 0)
        {
            GameManager.scriptActions.remainingPulses = 0;
            GameManager.scriptActions.HealActivate(false);

        }
        StopCoroutine(PostDeath());
        StartCoroutine(PostDeath());
    }

    IEnumerator PostDeath()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hp = hpMax;
        PlayerControls = true;


    }

    public void ToggleIFrames(bool toggleOn)
    {
        inIFrames = toggleOn;

        DamageCollider.enabled = !toggleOn;
        shaderAnimator.SetBool("damaged", toggleOn);

        if (toggleOn)
        {
            if (inCover) Cover(false);
            StopCoroutine(IFramesCount());
            StartCoroutine(IFramesCount());
        }

    }

    public void Cover (bool covering)
    {
        if (GameManager.GamePaused == false)
        {
            inCover = covering;

            CoverCollider.enabled = covering;
            DamageCollider.enabled = !covering;
            
            GameManager.scriptMovement.HaltMovement(covering);

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

    }

    public IEnumerator IFramesCount()
    {
        yield return new WaitForSeconds(iFramesSeconds);
        if (dead == false) ToggleIFrames(false);
    }


}


