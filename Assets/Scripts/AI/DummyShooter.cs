using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyShooter : MonoBehaviour
{

    [Header("Attack")]
    public int damage;
    public int knockback;
    public float speed;
    public GameObject projectile;
    public Transform projectileOrigin;

    public float attackCooldown;
    private float attackCooldownFrames;

    [Header("Detection")]
    public float detectionRadius;
    public CircleCollider2D detectionCollider;
    private RaycastHit2D[] detectedTargets;


    private void Attack(Vector3 target)
    {
        GameObject attack = Instantiate(projectile);
        attack.transform.position = projectileOrigin.transform.position;
        attack.GetComponent<Projectile>().damage = damage;
        attack.GetComponent<Projectile>().knockback = knockback;
        attack.GetComponent<Projectile>().speed = speed;

        //Difference between target and projectile origin position. Needed for calculations of projectile rotation.
        Vector3 difference = target - projectileOrigin.position;

        //Err....
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        attack.GetComponent<Projectile>().rotationZ = rotationZ;

        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        attack.GetComponent<Projectile>().direction = direction;

        attackCooldownFrames = attackCooldown;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackCooldownFrames <= 0)
        {
            Attack(new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y+1,0));
        }
    }

    private void FixedUpdate()
    {
        //Physics2D.CircleCast(projectileOrigin.position, detectionRadius, Vector2.zero, 0, detectedTargets);

        detectionCollider.radius = detectionRadius;
        if (attackCooldownFrames > 0) attackCooldownFrames--;    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 0, 0, 255);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
