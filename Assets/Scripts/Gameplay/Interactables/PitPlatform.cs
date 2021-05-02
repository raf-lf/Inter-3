using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitPlatform : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 positionChange;
    public float changeTime;
    public bool changedPosition;
 

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.transform.parent = this.gameObject.transform;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.transform.parent = null;

    }


    private void Update()
    {
        if (changedPosition)
        {
            transform.position = Vector2.Lerp(transform.position, startPosition + positionChange, changeTime);

        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, startPosition, changeTime);
        }
        
    }

}
