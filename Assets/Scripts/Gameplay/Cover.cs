using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public bool beingUsed;
    [SerializeField]
    private bool interactible = false;
    [SerializeField]
    private bool playerUsingCover = false;

    public GameObject popup;

    private void CoverUse(bool wentIn)
    {
        playerUsingCover = wentIn;

        GameManager.scriptPlayer.Cover(wentIn);

    }

    //Enables cover usage while inside collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (beingUsed == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                interactible = true;
                popup.SetActive(true);
            }
            else if (collision.gameObject.GetComponent<Soldier>())
            {
                collision.gameObject.GetComponent<Soldier>().coverInContact = GetComponent<Cover>();
            }
        }
    }

    //Disables cover usage when leaving collider. Doesn't disable while using cover
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (beingUsed == false)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                interactible = false;
                popup.SetActive(false);

                if (playerUsingCover) CoverUse(false);
            }
            else if (collision.gameObject.GetComponent<Soldier>())
            {
                collision.gameObject.GetComponent<Soldier>().coverInContact = null;
            }
        }
    }

    void Update()
    {
        //Can't tale cover while crouching
        if (interactible && GameManager.scriptMovement.crouching == false)
        {
            if (playerUsingCover)
            {
                //Gets out of cover. Can also use crouch key
                if (Input.GetKeyDown(PlayerActions.keyCover) || Input.GetKeyDown(PlayerActions.keyCrouch) || Input.GetKeyDown(PlayerActions.keyJump))
                {
                    CoverUse(false);
                }

            }
            else
            {
                //Gets in cover
                if (Input.GetKeyDown(PlayerActions.keyCover))
                // if (usingCover == false && Input.GetKeyDown(PlayerActions.keyCover))
                {   
                    CoverUse(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "icon_cover");
    }
}

/*
 * PODE SER UMA BOA COLOCAR UM COOLDOWN ENTRE USO DE COBERTURA, COLOCANDO TAMBÉM UMA ANIMAÇÃO
*/
