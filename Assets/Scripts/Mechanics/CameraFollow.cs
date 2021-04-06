using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static GameObject MainCamera;
    public float followSpeed;
    private Transform followTarget;
    public Vector3 offset;
    private Vector3 startOffset;
    
    void Start()
    {
        MainCamera = this.gameObject;
        startOffset = offset;
        //followTarget = Player.PlayerCharacter.transform;
    }

    void ChangeFocus(Vector2 newOffeset)
    {
        offset = newOffeset;
    }

    void ResetFocus()
    {
        offset = startOffset;
    }

    void Update()
    {
        //Try setting player character until it is set
        if (followTarget == null) followTarget = Player.PlayerCharacter.transform;
        if (followTarget != null) transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z) + offset, followSpeed);
        
    }
}
