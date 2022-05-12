using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    [SerializeField] Animator animator;
    private NavMeshAgent navMeshAgent;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination;
    }

    public void GetInRangeFrom(Vector3 destination, float range, out bool alreadyInRange)
    {
        if ((destination - transform.position).magnitude < range + 0.1f) // 0.1 margin to dodge NavMeshAgent’s lack of precision
        {
            alreadyInRange = true;
            navMeshAgent.isStopped = true;
        }
        else
        {
            alreadyInRange = false;
            MoveTo(destination + (transform.position - destination).normalized * range);
        }
    }
    public void GetInRangeFrom(Vector3 destination, float range)
    {
        GetInRangeFrom(destination, range, out bool dummy);
    }

    public void RotateTowards(Transform targetTransform, float interpolation=1)
    {
        // Bugged. Fix later.
        // Quaternion fullRotation = Quaternion.FromToRotation(transform.forward, targetTransform.position - transform.position);
        // transform.rotation = Quaternion.Lerp(transform.rotation, fullRotation, interpolation);

        // This will do for now
        transform.LookAt(targetTransform);
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Forward", navMeshAgent.velocity.magnitude);
    }
}
