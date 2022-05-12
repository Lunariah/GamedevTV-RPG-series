using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        if (target == null)
            target = FindObjectOfType<PlayerInput>().transform;
    }

    void LateUpdate()
    {
        transform.position = target.position;    
    }
}
