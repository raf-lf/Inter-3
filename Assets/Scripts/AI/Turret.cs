using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool active = true;
    public Animator animator;
    public GameObject turretHead;

    [Header("Attack")]
    public GameObject projectile;
    public Transform projectileOrigin;

    public float attackCooldown;
    public float attackCooldownFrames;
    public PlayAudio audioOn;
    public PlayAudio audioOff;


    private void Attack(Vector3 target)
    {
        animator.Play("attack");
        GameObject attack = Instantiate(projectile);
        attack.transform.position = projectileOrigin.transform.position;

        attack.GetComponent<Projectile>().rotationZ = Calculations.GetRotationZToTarget(turretHead.transform.position, target);
        attack.GetComponent<Projectile>().direction = Calculations.GetDirectionToTarget(projectileOrigin.position,target);

        attackCooldownFrames = attackCooldown;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            Vector3 targetToAttack = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 1, 0);

            turretHead.transform.rotation = Quaternion.Euler(0,0, Calculations.GetRotationZToTarget(turretHead.transform.position, targetToAttack));

            if (attackCooldownFrames <= 0)
            {
                Attack(targetToAttack);
            }
        }
    }

    private void FixedUpdate()
    {
        //Physics2D.CircleCast(projectileOrigin.position, detectionRadius, Vector2.zero, 0, detectedTargets);

        if (attackCooldownFrames > 0) attackCooldownFrames--;    
    }

}
