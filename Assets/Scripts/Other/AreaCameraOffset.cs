using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCameraOffset : MonoBehaviour
{
    public Vector3 offsetChange;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 change;
            change = GameManager.scriptCamera.offset + offsetChange;
            GameManager.scriptCamera.ChangeOffset(change);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.scriptCamera.ResetOffset();

        }

    }
}
