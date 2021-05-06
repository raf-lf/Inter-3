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
                DamagePlayer(collision.gameObject.GetComponentInParent<Player>());
            }

            if (collision.gameObject.GetComponentInParent<Creature>())
            {
                DamageCreature(collision.gameObject.GetComponentInParent<Creature>());
            }

    }

    protected virtual void DamagePlayer(Player script)
    {
        script.Damage(playerDamage, knockback, transform);
    }

    private void DamageCreature(Creature script)
    {
        script.Damage(enemyDamage, knockback, transform);
    }

}
