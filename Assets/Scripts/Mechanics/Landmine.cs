using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    private bool activating;
    public float explosionDelay;
    public GameObject explosionEffect;

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect);
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activating == false)
        {
            if (GameManager.scriptMovement.crouching == false && GameManager.scriptMovement.walking == false)
            {
                activating = true;
                GetComponent<Animator>().SetBool("active", true);
                Invoke("Explode", explosionDelay);
            }
        }
    }
}
