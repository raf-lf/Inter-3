using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionDelay;
    public GameObject explosionEffect;

    private void Awake()
    {
        Invoke("Explode", explosionDelay);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Creature>()) Explode();
        
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
        
    }
}
