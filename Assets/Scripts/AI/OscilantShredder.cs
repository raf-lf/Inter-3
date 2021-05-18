using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilantShredder : Creature
{

    [Header("Behavior")]
    public bool hiding;
    public bool onWatch;
    public float restDuration = 2;
    public float readyLungeDuration = 1;
    public float lungeForce;
    public GameObject damageVFX;
    public GameObject deathVFX;
    public float damagedKnockback;

    public GameObject overlord;

    [Header("Audio")]
    public AudioClip[] warning = new AudioClip[1];
    public AudioClip[] lunge = new AudioClip[2];
    public AudioClip[] dig = new AudioClip[1];


    public void SfxWarning()
    {
        playSFX(warning, 1, new Vector2(0.9f, 1.1f));
    }
    public void SfxLunge()
    {
        playSFX(lunge, 1, standartPitchVariance);
    }
    public void SfxDig()
    {
        playSFX(dig, 1, standartPitchVariance);
    }

    public override void Start()
    {
        base.Start();
        if (facingOpposite) moveDirection = new Vector2(-1, 0);
        else moveDirection = new Vector2(1, 0);

        if (hiding) anim.SetBool("hiding", true);

    }

    public void State_ReadyLunge()
    {
        onWatch = false;
        FaceTarget();
        anim.SetInteger("state", 2);
        StopAllCoroutines();
        StartCoroutine(DelayLunge());

    }

    IEnumerator DelayLunge()
    {
        yield return new WaitForSeconds(readyLungeDuration);
        State_Lunge(GameManager.PlayerCharacter);
    }

    public void State_Lunge(GameObject lungeTarget)
    {
        FaceTarget();
        anim.SetInteger("state", 3);

        //Get difference between self and target positions
        Vector3 difference = new Vector3(lungeTarget.transform.position.x, lungeTarget.transform.position.y + 1) - transform.position;
        
        //Aim self rotation at target position
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //Gets direction values for velocity
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        float lungeForceVariance = lungeForce * Random.Range(0.8f, 1.2f);

        //Applies lunge to direction of target
        rb.velocity = direction * lungeForceVariance;

    }

    public void State_Rest()
    {
        anim.SetInteger("state", 0);
        StopAllCoroutines();
        StartCoroutine(DelayPatrol());

    }

    IEnumerator DelayPatrol()
    {
        yield return new WaitForSeconds(restDuration * Random.Range(0.8f, 1.2f));

        anim.SetInteger("state", 1);
    }


    public override void Death()
    {
        StopAllCoroutines();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().SetBool("death", true);

        Instantiate(deathVFX, transform);

        if (overlord != null)
        {
            overlord.GetComponent<OscilantAnihilator>().avaiableMinions += 1;
            overlord.GetComponent<OscilantAnihilator>().minionList.Remove(gameObject);
        }
    }

    public override void damageFeedback()
    {
        onWatch = false;
        Instantiate(damageVFX, transform);
    }

    public bool TargetInsideAmbushArea()
    {
        if (Physics2D.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), new Vector2(3.2f, 2.4f), 0, detectionMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void finishedHiding()
    {
        hiding = false;

    }

    protected override void Update()
    {
        if (GameManager.CutscenePlaying == false)
        {
            if (hiding)
            {

                if (TargetInsideAmbushArea())
                {
                    anim.SetBool("hiding", false);
                    Invoke("finishedHiding", 1);
                    //  StopAllCoroutines();
                    // StartCoroutine(ReadyLunge(GameManager.PlayerCharacter));

                }
            }
            else if (anim.GetInteger("state") == 1)
            {
                if (TargetInsideDetection()) State_ReadyLunge();

                else if (onWatch == false)
                {
                    Move();

                    if (facingOpposite)
                    {
                        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, .33f, movementLayerMask))
                        {
                            //   Debug.Log("Changed direction to right.");
                            SwitchDirection();
                        }
                    }
                    else if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, .33f, movementLayerMask))
                    {
                        // Debug.Log("Changed direction to left.");
                        SwitchDirection();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,.3f);

        if (hiding) Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), new Vector2(3.2f, 2.4f));
       // Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
