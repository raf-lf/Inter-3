using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    public Transform followTarget;
    public Vector3 offset;
    private Vector3 startOffset;
    public Vector3 pauseOffsetValues = new Vector3 (-5,0,5);
    private Vector3 pauseOffset;

    void Start()
    {
        GameManager.scriptCamera = GetComponent<CameraFollow>();
        startOffset = offset;
        //followTarget = Player.PlayerCharacter.transform;
    }

    public void PauseCameraOffset(bool pause)
    {
        if (pause)
        {
            pauseOffset += pauseOffsetValues;
        }
        else
        {
            pauseOffset -= pauseOffsetValues;
        }

    }

    public void ChangeFocus(Vector2 newOffeset)
    {
        offset = newOffeset;
    }

    public void ResetFocus()
    {
        offset = startOffset;
    }

    void Update()
    {
        //Try setting player character until it is set
        if (followTarget == null) followTarget = GameManager.PlayerCharacter.transform;
        if (followTarget != null) transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z) + offset + pauseOffset, followSpeed);
        
    }
}
