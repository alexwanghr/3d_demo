using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth = 3;
    private GameObject hovercraft;
    private Vector3 targetPosition;
    
    Transform follow;
	
    void Start()
    {
        follow = GameObject.FindWithTag ("Player").transform;
    }
	
    void LateUpdate ()
    {
        targetPosition = new Vector3(follow.position.x, follow.position.y+2, follow.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}
