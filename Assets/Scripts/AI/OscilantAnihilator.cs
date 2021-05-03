using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilantAnihilator : Creature
{

    [Header("Behavior")]
    public bool active;
    private bool busy;
    public bool bossBatle;

    [Header("Attack")]
    public float attackRadius = 2;
    public float attackCooldown = 4;
    private float attackCooldownTimer;
    public float attackAnimationDuration = 2;

    [Header("Special")]
    public float minionSpawnCooldown = 10;
    private float minionSpawnCooldownTimer;
    public int avaiableMinions = 5;

    [Header("Components")]
    public GameObject damageVFX;
    public Animator eyeAnimator;
    public Animator bladeAnimator;
    public Rigidbody2D blades;
    public GameObject[] teleportVFX = new GameObject[2];
    public GameObject minion;
    private Vector3 minionSpawnPosition;
    public List<GameObject> minionList = new List<GameObject>();
    public ParticleSystem[] damageTierVfx = new ParticleSystem[3];
    private ParticleSystem.EmissionModule[] damageTierVfxEmission = new ParticleSystem.EmissionModule[3];



    public override void Start()
    {
        base.Start();
        for (int i = 0; i < damageTierVfx.Length; i++)
        {
            damageTierVfxEmission[i] = damageTierVfx[i].emission;
        }

    }

    
    public override void damageFeedback()
    {
        Instantiate(damageVFX, transform);
    }

    public override void Death()
    {
        StopAllCoroutines();
        eyeAnimator.SetInteger("state", 3);
        busy = true;
        bladeAnimator.SetInteger("state",0);
        anim.Play("death");

        /*
         * minionList.Sort();
        foreach (GameObject existingMinion in minionList)
        {
            existingMinion.GetComponent<OscilantDud>().Death();
        }
        */

        if (bossBatle)
        {
            StartCoroutine(DeathEnd());
            Player.PlayerControls = false;
            GameManager.scriptMovement.HaltMovement();
            GameManager.scriptCamera.followTarget = gameObject.transform;
        }

    }
    IEnumerator DeathEnd()
    {
        yield return new WaitForSeconds(8);
        Player.PlayerControls = true;
        GameManager.scriptCamera.followTarget = GameManager.PlayerCharacter.transform;
    }

    public void bladeRotationBoost(float force)
    {
        blades.angularVelocity -= force;

    }
    public void eyeAnimationControl (int state)
    {
        eyeAnimator.SetInteger("state", state);

    }

    private bool targetInAttackRange()
    {
        if (Physics2D.CircleCast(transform.position, attackRadius, Vector2.zero, 0, detectionMask)) return true;
        else return false;

    }

    public void Attack()
    {
        busy = true;
        eyeAnimator.SetInteger("state", 3);
        anim.Play("attack");
        bladeAnimator.Play("blades_attack");
        attackCooldownTimer = Time.time + attackCooldown;
        StopAllCoroutines();
        StartCoroutine(AttackEnd());

    }

    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(attackAnimationDuration);
        busy = false;
    }

    public void SpawnMinion()
    {
        avaiableMinions -= 1;
        eyeAnimator.SetInteger("state", 4);
        minionSpawnCooldownTimer = Time.time + minionSpawnCooldown;
        minionSpawnPosition = new Vector3(transform.position.x + Random.Range(-3.2f, 3.2f), transform.position.y + Random.Range(0, 3.2f),0);

        GameObject teleportCast = Instantiate(teleportVFX[1], eyeAnimator.gameObject.transform);

        GameObject teleportTarget = Instantiate(teleportVFX[0]);
        teleportTarget.transform.position = minionSpawnPosition;

        Invoke("finishedSpawning", 1f);

    }

    private void finishedSpawning()
    {
        GameObject createdMinion = Instantiate(minion);
        createdMinion.GetComponent<OscilantDud>().overlord = gameObject;
        minionList.Add(createdMinion);
        createdMinion.transform.position = minionSpawnPosition;
        eyeAnimator.SetInteger("state", 1);

    }

    private void finishedActivating()
    {
        active = true;
        bladeAnimator.SetInteger("state", 1);

    }


    private void Update()
    {
            if (hp < hpMax * 0.75 && hp>0) damageTierVfxEmission[0].enabled = true;
            else damageTierVfxEmission[0].enabled = false;

            if (hp < hpMax * 0.5 && hp > 0) damageTierVfxEmission[1].enabled = true;
            else damageTierVfxEmission[1].enabled = false;

            if (hp < hpMax * 0.25 && hp > 0) damageTierVfxEmission[2].enabled = true;
            else damageTierVfxEmission[2].enabled = false;

        if (active == false)
        {
            if (TargetInsideDetection() && anim.GetBool("active") == false)
            {
                anim.SetBool("active", true);
                eyeAnimator.SetInteger("state", 1);
                Invoke("finishedActivating", 1);

            }

        }
        else if (busy == false)
        {
            if(Time.time > minionSpawnCooldownTimer && hp <= hpMax*0.5f && hp>0)
            {
                if (avaiableMinions>0) SpawnMinion();

            }

            if (targetInAttackRange() == false)
            {
                if (transform.position.x > GameManager.PlayerCharacter.transform.position.x)
                {
                    moveDirection = new Vector2(-1, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    moveDirection = new Vector2(1, 0);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                Move();
                bladeAnimator.SetInteger("state", 1);
                eyeAnimator.SetInteger("state", 1);

            }
            else 
            { 
                StopMove();
                bladeAnimator.SetInteger("state", 2);
                eyeAnimator.SetInteger("state", 2);
                if (Time.time > attackCooldownTimer) Attack();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,1,0,1f);

        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), attackRadius);
       // Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
