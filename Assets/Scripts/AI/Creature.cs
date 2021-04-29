using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Move(Vector2 direction, Rigidbody2D rb, float speed);

}

public interface IDamageable
{
    void Damage(int iHpLoss, float iKnockback, Transform iSourcePosition);

}

public abstract class Creature : MonoBehaviour
{
    public int hp;
    public int hpMax;

    public static void Move(Vector2 direction, float speed, GameObject creature)
    {
        creature.GetComponent<Rigidbody2D>().velocity = direction * speed;
        creature.GetComponent<Animator>().SetBool("move", true);
    }

    public static void StopMove(GameObject creature)
    {
        creature.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        creature.GetComponent<Animator>().SetBool("move", false);
    }


    public void Damage(int hpLoss, float knockback, Transform sourcePosition)
    {
        ChangeHp(hpLoss * -1);


        if (knockback != 0)
        {
            GetComponent<PlayerMovement>().Knockback(knockback, sourcePosition);
        }
    }

    public void Heal(int hpGain)
    {
        ChangeHp(hpGain);
    }

    public void ChangeHp(int change)
    {
        if (hp + change < 0) hp = 0;
        else if (hp + change > hpMax) hp = hpMax;
        else hp += change;

        if (hp == 0) Death();

    }

    public void Death()
    {
    }

}