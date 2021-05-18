using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected bool paused;
    protected bool dying;

    [Header("Atributes")]
    public int hp;
    public int hpMax;
    [Range(0,10)]
    public int knockbackEffect = 10;

    [Header("Detection")]
    public float detectionRadius;
    public LayerMask detectionMask;
    public GameObject target;

    [Header("Movement")]
    public Vector2 moveDirection;
    public float speedIncrement;
    public float speedMax;
    private float speedChange;
    public bool moveAxisY;
    public bool moving;
    public bool facingOpposite;
    protected LayerMask movementLayerMask;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public bool spriteFacingLeft;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource sfxLoopSource;
    public Vector2 standartPitchVariance = new Vector2(.7f, 1.3f);


    public void playSFX(AudioClip[] audioClip, float volume, Vector2 pitchVariance)
    {
        sfxSource.volume = volume * GameManager.scriptAudio.volumeSfx;
        sfxSource.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        sfxSource.PlayOneShot(audioClip[(int)Random.Range(0, audioClip.Length)]);

    }

    public virtual void Start()
    {
        hp = hpMax;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movementLayerMask = LayerMask.GetMask("Default");
        target = GameManager.PlayerCharacter;

    }

    public virtual void Move()
    {
        anim.SetBool("move", true);

        /*
        if (speedCheckAxisY)
        {
            speedChange = rb.velocity.y + speedIncrement;
            if (facingOpposite) speedChange = rb.velocity.y - speedIncrement;
        }
        else
        {
            speedChange = rb.velocity.x + speedIncrement;
            if (facingOpposite) speedChange = rb.velocity.x - speedIncrement;
        }

        if (facingOpposite)
        {
            if (speedChange < -speedMax) speedChange = -speedMax + speedChange;
        }
        else
        {
            if (speedChange > speedMax) speedChange = speedMax - speedChange;
        }
        */

       // Vector2 velocity = moveDirection * speedChange;
        Vector2 velocity = moveDirection * speedIncrement;
            
        if (moveAxisY)  rb.velocity = new Vector2(rb.velocity.x, velocity.y);
        else rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }

    public void SwitchDirection()
    {
        facingOpposite = !facingOpposite;
        ChangeDirection(facingOpposite);

    }

    public void FaceTarget()
    {
        if (transform.position.x > target.transform.position.x) ChangeDirection(true);
        else ChangeDirection(false);

    }

    public void ChangeDirection(bool oppositeWay)
    {
        facingOpposite = oppositeWay;

        if (facingOpposite)
        {
            if (spriteFacingLeft == false) transform.rotation = Quaternion.Euler(0, 180, 0);
            else transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            if (spriteFacingLeft == false) transform.rotation = Quaternion.Euler(0, 0, 0);
            else transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (moveAxisY == false)
        {
            moveDirection.x = Mathf.Abs(moveDirection.x);
            if (oppositeWay) moveDirection.x *= -1;
        }
        else
        {
            moveDirection.y = Mathf.Abs(moveDirection.y);
            if (oppositeWay) moveDirection.y *= -1;
        }


    }

    public virtual void StopMove()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().SetBool("move", false);
    }

    public virtual void damageFeedback()
    {

    }

    public virtual void Damage(int hpLoss, float knockback, Transform sourcePosition)
    {
        if (hpLoss > 0) damageFeedback();
        knockback *= knockbackEffect / 10;
        rb.velocity = knockback * Calculations.GetDirectionToTarget(sourcePosition.position, transform.position);

        ChangeHp(hpLoss * -1);
        Debug.Log(gameObject.name + " took " + hpLoss + " damage. " + hp + " hp remaining.");

    }
    public virtual void Heal(int hpGain)
    {
        ChangeHp(hpGain);
    }

    public virtual void ChangeHp(int change)
    {
        hp += change;
        Mathf.Clamp(hp, 0, hpMax);

        if (hp <= 0) Death();

    }
    public bool TargetInsideDetection()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionRadius, detectionMask))
        {
          //  Debug.Log("Something detected!");
            return true;
        } 
        else return false;
    }

    public virtual void Death()
    {
        rb.velocity = Vector2.zero;

        dying = true;
        anim.Play("death");
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    protected virtual void Update()
    {
        paused = GameManager.CutscenePlaying;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), detectionRadius);
    }
}