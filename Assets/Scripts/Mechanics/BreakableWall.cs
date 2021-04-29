using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject breakVFX;
    public GameObject resistVFX;
    public Transform vfxOrigin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breaker"))
        {
            if (GetComponentInChildren<ObjectTagInArea>().active)
            {
                GameObject vfx = Instantiate(breakVFX);
                vfx.transform.position = vfxOrigin.transform.position;
                vfx.transform.rotation = vfxOrigin.transform.rotation;
                Destroy(this.gameObject);

            }
            else
            {
                GameObject vfx = Instantiate(resistVFX);
                vfx.transform.position = vfxOrigin.transform.position;
                vfx.transform.rotation = vfxOrigin.transform.rotation;
            }
        }
       
    }


}
