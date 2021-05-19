using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour
{
    public float warningTime;
    public GameObject destructionVfx;
    public Animator animator;
    public LayerMask maskDestruction;
    public BoxCollider2D damageCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetInteger("state") == 0)
        {
            if (collision.gameObject.GetComponent<Player>() || collision.gameObject.GetComponent<Creature>())
            {
                animator.SetInteger("state", 1);
                StartCoroutine(Fall());

            }
        }

    }

    private void Destroy()
    {
        Destroy(gameObject);
        
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(warningTime);
        GetComponent<Rigidbody2D>().isKinematic = false;
        animator.SetInteger("state",2);
    }

    private void Update()
    {
        if (Physics2D.OverlapBox(damageCollider.transform.position, damageCollider.size/2, 0, maskDestruction) && animator.GetInteger("state") == 2)
        {
            animator.SetInteger("state", 3);
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        }
    }

}
