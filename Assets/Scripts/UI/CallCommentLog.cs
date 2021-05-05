using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCommentLog : MonoBehaviour
{
    public bool off;

    [Header("IDs")]
    public int level;
    public int chat;

    [Header("Log")]
    public bool isLogMessage;
    public string logMessage;

    public void Comment()
    {
        GameManager.scriptComment.Write(level, chat, 1);
    }

    public void Log()
    {
        GameManager.scriptLog.Write(logMessage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (off == false && collision.gameObject.CompareTag("Player"))
        {
            if(isLogMessage) Log();
            else Comment();
           
            off = true;

        }
        
    }

    private void OnDrawGizmos()
    {
        if (isLogMessage) Gizmos.DrawIcon(transform.position, "icon_chatter", true);
        else Gizmos.DrawIcon(transform.position, "icon_chatter", true);
    }
}
