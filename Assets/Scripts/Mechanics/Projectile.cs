using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public int knockback;
    public float speed;

    public float rotationZ;
    public Vector2 direction;

    public float autoDestroyTimer;
    private float timer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        rb.velocity = direction * speed;

        timer = Time.time + autoDestroyTimer;
    }

    private void Impact()
    {
        GetComponent<Animator>().SetInteger("state", 1);
        rb.velocity = new Vector2(0,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Impact();

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(damage, knockback, transform);

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemyDamageInterface = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
            if (enemyDamageInterface != null) enemyDamageInterface.Damage(damage, knockback, transform);

        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Time.time >= timer) GetComponent<Animator>().SetInteger("state", 2);
    }
}
