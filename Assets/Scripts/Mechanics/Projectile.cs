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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponentInParent<Player>())
        {
            Impact();
            collision.gameObject.GetComponentInParent<Player>().Damage(damage, knockback, transform);

        }
        else if (collision.gameObject.GetComponentInParent<Creature>())
        {
            Impact();
            collision.gameObject.GetComponentInParent<Creature>().Damage(damage, knockback, transform);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Impact();
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
