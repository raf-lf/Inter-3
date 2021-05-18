using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Creature
{
    [Header("Creature Specific")]
    [Space(30)]
    [SerializeField]
    private bool agressive;
    public int state;

    [Header("Attack")]
    public Vector2 shots;
    [SerializeField]
    private int shotsRemaining;
    public float shotInterval;
    private float shotIntervalTimer;
    public GameObject projectile;
    public Transform projectileOrigin;
    public GameObject weapon;

    [Header("Rest")]
    public float restTime;
    private float restEndTargetTime;

    [Header("Flee")]
    public float fleeDistance;
    public float fleeTime = 3;
    private float fleeTargetTime;
    public bool stayPut;

    [Header("Cover")]
    public Cover coverInContact;
    public Vector2 coverTime;
    private float coverLeaveTargetTime;



    public override void Start()
    {
        base.Start();
        target = GameManager.PlayerCharacter;

    }

    private void BeginRest()
    {
        restEndTargetTime = Time.time + restTime;

    }

    private void BeginAttack()
    {
        shotsRemaining = (int)Random.Range(shots.x, shots.y+1);

    }

    private void EndAttack()
    {
        if (facingOpposite) weapon.transform.rotation = Quaternion.Euler(0,180,0);
        else weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        BeginRest();

    }

    private void Shoot()
    {
        FaceTarget();
        shotsRemaining--;
        shotIntervalTimer = Time.time + shotInterval;

        Vector2 shotTarget = target.transform.position;
        shotTarget.y += 1;

        float rotationZ = Calculations.GetRotationZToTarget(projectileOrigin.position, shotTarget);
                
        if (rotationZ > 90 || rotationZ < -90) weapon.transform.rotation = Quaternion.Euler(0, 180, -rotationZ + 180);
        else weapon.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        GameObject shot = Instantiate(projectile);
        shot.transform.position = projectileOrigin.position;
        shot.GetComponent<Projectile>().rotationZ = Calculations.GetRotationZToTarget(projectileOrigin.position, shotTarget);
        shot.GetComponent<Projectile>().direction = Calculations.GetDirectionToTarget(projectileOrigin.position, shotTarget);

    }

    private void BeginFlee()
    {
        fleeTargetTime = Time.time + fleeTime;
    }

    private void Flee()
    {
        if (Vector2.Distance(target.transform.position, transform.position) < fleeDistance)
        {
            if (target.transform.position.x > transform.position.x && facingOpposite == false)
            {
                ChangeDirection(true);

            }
            else if (target.transform.position.x < transform.position.x && facingOpposite == true)
            {
                ChangeDirection(false);
            }

            if (stayPut == false) Move();
        }
    }

    private void BeginCover()
    {
        coverInContact.beingUsed = true;
        rb.velocity = Vector2.zero;
        coverLeaveTargetTime = Time.time + Random.Range(coverTime.x, coverTime.y);

        SpriteRenderer[] spriteGroup = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spriteGroup)
        {
            sprite.sortingLayerName = ("Cover");

        }

    }

    private void EndCover()
    {
        coverInContact.beingUsed = false;

        SpriteRenderer[] spriteGroup = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spriteGroup)
        {
            sprite.sortingLayerName = ("Default");

        }

    }

    public override void Death()
    {
        if (state == 4) EndCover();
        base.Death();

    }

    public override void damageFeedback()
    {
        base.damageFeedback();
        detectionRadius = 14;
    }

    protected override void Update()
    {
        base.Update();

        anim.SetInteger("state", state);

        if (paused == false && dying == false)
        {
            if (agressive == false)
            {
                if (TargetInsideDetection()) agressive = true;

            }
            else
            {
                if (state == 0)
                {
                    if (TargetInsideDetection())
                    {
                        state = 1;
                        BeginAttack();
                    }
                }
                else if (state == 1)
                {
                    if (shotsRemaining > 0)
                    { 
                        if(Time.time >= shotIntervalTimer) Shoot();
                    }

                    else
                    {
                        EndAttack();
                        state = 2;
                    }
                }

                else if (state == 2 && Time.time > restEndTargetTime)
                {
                    BeginFlee();
                    state = 3;
                }
                else if (state == 4 && Time.time > coverLeaveTargetTime)
                {
                    EndCover();
                    state = 0;
                }
                else if (state == 3)
                {
                    //Attempts to hide in cover only if touching an empty cover and if the player is not nearby
                    if (coverInContact != null && coverInContact.beingUsed == false && Vector2.Distance(target.transform.position, transform.position) > fleeDistance)
                    {
                        state = 4;
                        BeginCover();
                    }
                    else if (Time.time < fleeTargetTime) Flee();
                    else state = 0;
                }
            }
        }
    }
}
