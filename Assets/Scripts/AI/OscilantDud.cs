using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilantDud : Creature
{
    [Header("Behavior")]
    private int state = 1;
    public float idleTime = 2;
    public float lungePrepTime = 1;
    public GameObject damageVFX;
    public GameObject deathVFX;
    public float damagedKnockback;

    [Header("Specific")]
    public float lungeForce;
    public bool unmoving;
    private LayerMask movementLayerMask;


    public override void Start()
    {
        base.Start();
        movementLayerMask = LayerMask.GetMask("Default");

    }

    private void FaceTarget(GameObject target)
    {
        if (target.transform.position.x > transform.position.x && facingOpposite) ChangeDirection();
        else if (target.transform.position.x < transform.position.x && facingOpposite == false) ChangeDirection();

    }
    IEnumerator ReadyLunge(GameObject target)
    {
        FaceTarget(target);
        state = 2;
        GetComponent<Animator>().SetInteger("state", 2);
        yield return new WaitForSeconds(lungePrepTime);
        Lunge(target);
    }

    private void Lunge(GameObject target)
    {
        unmoving = false;
        FaceTarget(target);
        state = 3;
        GetComponent<Animator>().SetInteger("state", 3);

        //Get difference between self and target positions
        Vector3 difference = new Vector3(target.transform.position.x, target.transform.position.y + 1) - transform.position;
        
        //Aim self rotation at target position
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //Gets direction values for velocity
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        float lungeForceVariance = lungeForce * Random.Range(0.8f, 1.2f);

        //Applies lunge to direction of target
        GetComponent<Rigidbody2D>().velocity = direction * lungeForceVariance;

    }

    IEnumerator ReadyIdle()
    {
        state = 0;
        GetComponent<Animator>().SetInteger("state", 0);

        float idleTimeVariance = idleTime * Random.Range(0.8f, 1.2f);
        yield return new WaitForSeconds(idleTimeVariance);

        state = 1;
        GetComponent<Animator>().SetInteger("state", 1);
    }

    public void ForceIdle()
    {
        StopAllCoroutines();
        StartCoroutine(ReadyIdle());
    }

    public override void damageFeedback()
    {
        unmoving = false;
        Instantiate(damageVFX, transform);
        rb.velocity += Vector2.up * damagedKnockback;
    }


    public override void Death()
    {
        state = 5;
        StopAllCoroutines();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().SetBool("death", true);

        Instantiate(deathVFX, transform);
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 0, 0, 255);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && state == 1)
            {

            StopAllCoroutines();
            StartCoroutine(ReadyLunge(collision.gameObject));
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (state == 3) StartCoroutine(ReadyIdle());

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.otherCollider.gameObject.CompareTag("Attack"))
            {
                collision.gameObject.GetComponent<Player>().Damage(damage, knockback, transform);

            }

        }

        private void FixedUpdate()
        {
            //Constantly
            if (state == 1 && unmoving == false)
            {
                Move();

                if (facingOpposite)
                {
                    if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y ), Vector2.left, .33f, movementLayerMask))
                    {
                     //   Debug.Log("Changed direction to right.");
                        ChangeDirection();
                    }
                }
                else if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, .33f, movementLayerMask))
                {
                   // Debug.Log("Changed direction to left.");
                    ChangeDirection();
                }
            }

        }

    }
