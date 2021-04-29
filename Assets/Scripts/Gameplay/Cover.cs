using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField]
    private bool interactible = false;
    [SerializeField]
    private bool usingCover = false;

    public GameObject popup;

    private void CoverUse(bool wentIn)
    {
        usingCover = wentIn;

        Player.scriptPlayer.Cover(wentIn);

    }

    //Enables cover usage while inside collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactible = true;
            popup.SetActive(true);
        }
    }

    //Disables cover usage when leaving collider. Doesn't disable while using cover
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactible = false;
            popup.SetActive(false);
            
            if(usingCover) CoverUse(false);
        }
    }

    void Update()
    {
        //Can't tale cover while crouching
        if (interactible && Player.scriptMovement.crouching == false)
        {
            if (usingCover)
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
