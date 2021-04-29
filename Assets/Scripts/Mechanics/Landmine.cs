using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float explosionDelay;
    public GameObject explosionEffect;

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect);
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("active", true);
            Invoke("Explode", explosionDelay);
        }
    }
}
