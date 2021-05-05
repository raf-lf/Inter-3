using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    private float startFollowSpeed;
    public Transform followTarget;
    public Vector3 offset;
    private Vector3 startOffset;
    public Vector3 pauseOffsetValues = new Vector3(-5,0,5);
    private Vector3 pauseOffset;

    void Start()
    {
        GameManager.scriptCamera = GetComponent<CameraFollow>();
        startOffset = offset;
        startFollowSpeed = followSpeed;
        //followTarget = Player.PlayerCharacter.transform;
    }

    public void ChangeOffset(Vector3 offsetChange)
    {
        offset = offsetChange;

    }

    public void ResetOffset()
    {
        offset = startOffset;

    }

    public void ChangeTarget(Transform target)
    {
        followTarget = target;

    }

    public void ResetTarget()
    {
        followTarget = GameManager.PlayerCharacter.transform;

    }
    public void ResetLerpSpeed()
    {
        followSpeed = startFollowSpeed;

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

    void Update()
    {
        //Try setting player character until it is set
        if (followTarget == null) followTarget = GameManager.PlayerCharacter.transform;
        if (followTarget != null) transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z) + offset + pauseOffset, followSpeed);
        
    }
}
