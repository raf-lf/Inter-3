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


    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        GetComponent<Rigidbody2D>().velocity = direction * speed;

        timer = Time.time + autoDestroyTimer;
    }

    private void Impact()
    {
        GetComponent<Animator>().SetInteger("state", 1);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
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
            Creature script = collision.gameObject.GetComponent<Creature>();
            if (script != null) script.Damage(damage, knockback, transform);

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
