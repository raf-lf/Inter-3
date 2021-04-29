using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int playerDamage;
    public int enemyDamage;
    public float knockback;
    public float damageDelay;
    public GameObject affectedPlayer;
    public List<GameObject> affectedCreatures = new List<GameObject>();

    private void Awake()
    {
        Invoke("Damage", damageDelay);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Player>())
        {
            affectedPlayer = collision.gameObject;
            //Debug.Log(collision.gameObject + " affected by AoE damage.");
        }

        if (collision.gameObject.GetComponentInParent<Creature>())
        {
            affectedCreatures.Add(collision.gameObject);
            //Debug.Log(collision.gameObject + " affected by AoE damage.");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Player>())
        {
            affectedPlayer = null;
        }

        if (collision.gameObject.GetComponentInParent<Creature>())
        {
            affectedCreatures.Remove(collision.gameObject);
        }
    }

    public void Damage()
    {
        if (affectedPlayer != null) affectedPlayer. GetComponentInParent<Player>().Damage(playerDamage, knockback, transform);

        if (affectedCreatures != null)
        {
            foreach (GameObject target in affectedCreatures)
            {
                if (target.GetComponentInParent<Creature>())
                {
                    target.GetComponentInParent<Creature>().Damage(enemyDamage, knockback, transform);
                }
            }

        }

    }
}
