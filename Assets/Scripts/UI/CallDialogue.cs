using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{
    public int level;
    public int dialogue;
    public int line;
    public bool isChatter;

    public void Dialogue()
    {
        WriteText.scriptWrite.DialogueWrite(level, dialogue, line);
    }

    public void Chatter()
    {
        WriteText.scriptWrite.ChatterWrite(level, dialogue, line);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isChatter) Chatter();
            else Dialogue();
            
            gameObject.SetActive(false);

        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255,255,255,150);
        if (isChatter) Gizmos.DrawIcon(transform.position,"icon_chatter",true);
        else Gizmos.DrawIcon(transform.position, "icon_dialogue",true);
    }
}
