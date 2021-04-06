using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public float followSpeed;
    public Transform followTarget;
    public Vector3 offset;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(followTarget.position.x, followTarget.position.y, followTarget.position.z) + offset, followSpeed);
        
    }
}
