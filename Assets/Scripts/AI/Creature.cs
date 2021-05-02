using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [Header("Atributes")]
    public int hp;
    public int hpMax;

    [Header("Attack")]
    public float detectionRadius;
    public CircleCollider2D detectionTrigger;
    public LayerMask detectionMask;

    [Header("Movement")]
    public Vector2 moveDirection;
    public float speedIncrement;
    public float speedMax;
    private float speedChange;
    public bool moveAxisY;
    public bool moving;
    [SerializeField]
    protected bool facingOpposite;


    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;


    public virtual void Start()
    {
        hp = hpMax;
        detectionTrigger.radius = detectionRadius;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    public virtual void ChangeDirection()
    {
        if (facingOpposite) transform.rotation = Quaternion.Euler(0, 0, 0);
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        facingOpposite = !facingOpposite;
        moveDirection *= -1;

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
        if (hpLoss>0) damageFeedback();
        ChangeHp(hpLoss * -1);

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
        if (Physics2D.OverlapCircle(transform.position, detectionRadius, detectionMask)) return true;
        else return false;
    }

    public virtual void Death()
    {
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), detectionRadius);
    }
}