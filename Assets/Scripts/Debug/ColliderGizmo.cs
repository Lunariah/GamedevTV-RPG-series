using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ColliderGizmo : MonoBehaviour
{
    [SerializeField]
    bool display = true;
    [SerializeField]
    Color color = Color.yellow;

    new Collider collider;
    System.Type colliderType;

    void OnValidate()
    {
        collider = GetComponent<Collider>();
        colliderType = collider.GetType();
    }
    void OnDrawGizmos()
    {
        if (display == false)
            return;

        Gizmos.color = color;

        if (colliderType == typeof(BoxCollider)) {
            Gizmos.DrawWireCube(transform.position + ((BoxCollider)collider).center, ((BoxCollider)collider).size);
        }
        else if (colliderType == typeof(SphereCollider)) {
            Gizmos.DrawWireSphere(((SphereCollider)collider).center, ((SphereCollider)collider).radius);
        }
        else if (colliderType == typeof(MeshCollider)) {
            Debug.Log("MeshCollider Gizmo unimplemented");
        }
        else if (colliderType == typeof(CapsuleCollider)) {
            Debug.Log("CapsuleCollider Gizmo unimplemented");
        }
    }
}
