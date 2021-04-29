using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    /*
    public int damage;
    public float speed;
    public float autoDestroyTimer;

    private Rigidbody2D rb;
    private float velX;
    private float velY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Rigidbody2D playerRB = Player.PlayerCharacter.GetComponent<Rigidbody2D>();
        velX += playerRB.velocity.x;
        velY += playerRB.velocity.y;
        autoDestroyTimer *= 60;
    }

    public void DirectionSpeed(int direction)
    {
        switch (direction)
        {
            case 1:
                velX += 0;
                velY += speed;
                break;
            case 2:
                velX += speed / 2;
                velY += speed / 2;
                break;
            case 3:
                velX += speed;
                velY += 0;
                break;
            case 4:
                velX += speed / 2;
                velY += -speed / 2;
                break;
            case 5:
                velX += 0;
                velY += -speed;
                break;
            case 6:
                velX += -speed / 2;
                velY += -speed / 2;
                break;
            case 7:
                velX += -speed;
                velY += 0;
                break;
            case 8:
                velX += -speed / 2;
                velY += speed / 2;
                break;
        }

        rb.velocity = new Vector2(velX, velY);
    }

    public void Blast()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Blast();
    }

    void FixedUpdate()
    {
        if (autoDestroyTimer <= 0) Blast();
        else autoDestroyTimer--;
    }
    */
}
