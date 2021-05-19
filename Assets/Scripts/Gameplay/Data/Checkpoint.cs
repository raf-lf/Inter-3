using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointId;

    private void Start()
    {
        //Destroys this checkpoint if it was already used, but is not current checkpoint.
        if (SaveDataManager.checkpointsUsed.Contains(checkpointId) && SaveDataManager.currentCheckpointId != checkpointId) Destroy(gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && SaveDataManager.currentCheckpointId != checkpointId)
        {
           // GameManager.scriptLog.Write("Checkpoint salvo!");
            SaveDataManager.SetCheckpoint(checkpointId, transform.position);
        }
    }
}
