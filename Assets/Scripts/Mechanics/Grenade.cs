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

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect);
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
        
    }
}
