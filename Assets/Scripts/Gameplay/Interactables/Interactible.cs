using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public bool oneUse;
    [SerializeField]
    private bool unusable;
    public GameObject canUseFeedback;

    [SerializeField]
    private bool interactible = false;

    public PlayAudio useSfxFeedback;

    public virtual void Interact()
    {
        Debug.Log(gameObject.name + " was interacted with!");

        if (useSfxFeedback != null) useSfxFeedback.playSFX();
        
    }

    public void Interactibility(bool turnOn)
    {
        interactible = turnOn;
        canUseFeedback.SetActive(turnOn);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && unusable == false)
        {
            Interactibility(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interactibility(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(PlayerActions.keyInteract) && interactible && unusable == false && Player.PlayerControls && GameManager.GamePaused == false)
        {
            if (oneUse)
            {
                unusable = true;
                Interactibility(false);
            }

            Interact();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "icon_interactible");
    }
}


