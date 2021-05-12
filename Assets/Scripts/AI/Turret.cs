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
    private float rotationZ;
    private Vector3 difference;
    private Vector3 direction;
    public PlayAudio audioOn;
    public PlayAudio audioOff;


    private void Attack(Vector3 target)
    {
        animator.Play("attack");
        GameObject attack = Instantiate(projectile);
        attack.transform.position = projectileOrigin.transform.position;


        attack.GetComponent<Projectile>().rotationZ = rotationZ;
        attack.GetComponent<Projectile>().direction = direction;

        attackCooldownFrames = attackCooldown;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            Vector3 targetPosition = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 1, 0);
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float distance = difference.magnitude;
            direction = difference / distance;
            direction.Normalize();

            turretHead.transform.rotation = Quaternion.Euler(0,0,rotationZ);
            difference = targetPosition - projectileOrigin.position;

            if (attackCooldownFrames <= 0)
            {
                Attack(targetPosition);
            }
        }
    }

    private void FixedUpdate()
    {
        //Physics2D.CircleCast(projectileOrigin.position, detectionRadius, Vector2.zero, 0, detectedTargets);

        if (attackCooldownFrames > 0) attackCooldownFrames--;    
    }

}
