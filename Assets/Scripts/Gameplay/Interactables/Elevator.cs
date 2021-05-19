﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool changedPosition;
    public Transform destinyTransform;
    public float moveSpeed = 0.1f;
    public AudioSource movingSfx;

    private Vector3 startPosition;
    private Vector3 otherPosition;
    [SerializeField]
    private float currentSfxVolume = 0;
    private bool moving;
 

    private void Start()
    {
        startPosition = transform.position;
        otherPosition = destinyTransform.position;

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
        if (changedPosition) transform.position = Vector3.MoveTowards(transform.position, otherPosition, moveSpeed);
        else transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed);

        if (changedPosition && transform.position != otherPosition) moving = true;
        else if (changedPosition == false && transform.position != startPosition) moving = true;
        else moving = false;

        if (moving) currentSfxVolume += .05f;
        else currentSfxVolume -= .05f;

        currentSfxVolume = Mathf.Clamp(currentSfxVolume, 0, 1);

        movingSfx.volume = currentSfxVolume * GameManager.volumeSFX;
    }

}
