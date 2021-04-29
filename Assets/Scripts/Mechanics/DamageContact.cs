using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContact : MonoBehaviour
{
    public int playerDamage;
    public int enemyDamage;
    public float knockback;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.GetComponentInParent<Player>())
            {
                Debug.Log(collision.gameObject.name + " took " + playerDamage + " damage.");
                collision.gameObject.GetComponentInParent<Player>().Damage(playerDamage, knockback, transform);
            }

            if (collision.gameObject.GetComponentInParent<Creature>())
            {
                Debug.Log(collision.gameObject.name + " took " + enemyDamage + " damage.");
                collision.gameObject.GetComponentInParent<Creature>().Damage(enemyDamage, knockback, transform);
            }

    }

}
