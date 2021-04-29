using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public int damage;
    public float knockback;
    public Transform sourcePosition;
    public bool playerOnly = false;
    public bool isTrigger = false;

    public Collider2D AreaCollider;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isTrigger == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Effect(collision.gameObject, true);
            }

            else if (playerOnly == false && collision.gameObject.CompareTag("Enemy"))
            {
                Effect(collision.gameObject, false);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isTrigger == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Effect(collision.gameObject, true);
            }

            else if (playerOnly == false && collision.gameObject.CompareTag("Enemy"))
            {
                Effect(collision.gameObject, false);
            }
        }
    }

    private void Effect(GameObject target, bool isPlayer)
    {
        if(isPlayer)
        {
             target.GetComponent<Player>().Damage(damage, knockback, sourcePosition);
        }

        else
        {

        }

    }

}
