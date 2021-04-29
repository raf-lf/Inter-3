using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool facingLeft;
    [SerializeField]
    private bool interactible = false;

    private void ClimbOn()
    {
        if (facingLeft)
        {
            GameManager.PlayerCharacter.transform.rotation = new Quaternion(0, 180, 0, 0);
            PlayerMovement.facingLeft = true;
        }
        else
        {
            GameManager.PlayerCharacter.transform.rotation = new Quaternion(0, 0, 0, 0);
            PlayerMovement.facingLeft = false;

        }
        GameManager.PlayerCharacter.transform.position = new Vector3(transform.position.x, GameManager.PlayerCharacter.transform.position.y, GameManager.PlayerCharacter.transform.position.z);
        GameManager.scriptMovement.HaltMovement();
        GameManager.scriptMovement.Climb(true);
    }

    //Enables cover usage while inside collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) interactible = true;
    }

    //Disables cover usage when leaving collider. Doesn't disable while using cover
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactible = false;
            if (GameManager.scriptMovement.climbing) GameManager.scriptMovement.Climb(false);
        }
    }

    void Update()
    {
        if (interactible && Player.PlayerControls && Player.CantAct == false && GameManager.scriptMovement.climbing == false && GameManager.scriptMovement.crouching == false && GameManager.scriptPlayer.inCover == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) ClimbOn();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "icon_interactible");
    }
}
