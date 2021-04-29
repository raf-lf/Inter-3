using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{
    public int level;
    public int dialogue;
    public int line;
    public int narrativeType;
    public string logMessage;

    public void Dialogue()
    {
        WriteText.scriptWrite.DialogueWrite(level, dialogue, line);
    }

    public void Chatter()
    {
        WriteText.scriptWrite.ChatterWrite(level, dialogue, line);
    }
    public void Log()
    {
        WriteText.scriptWrite.LogWrite(logMessage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (narrativeType)
            {
                case 1:
                    Dialogue();
                    break;
                case 2:
                    Chatter();
                    break;
                case 3:
                    Log();
                    break;
            }
            
            gameObject.SetActive(false);

        }
        
    }

    private void OnDrawGizmos()
    {
        
        switch (narrativeType)
        {
            case 1:
                Gizmos.DrawIcon(transform.position, "icon_dialogue", true);
                break;
            case 2:
                Gizmos.DrawIcon(transform.position, "icon_chatter", true);
                break;
            case 3:
                Gizmos.DrawIcon(transform.position, "icon_chatter", true);
                break;
        }
    }
}
