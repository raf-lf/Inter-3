using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    public int areaFloorId;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.scriptAudio.floorType = areaFloorId;

        }

    }

}
