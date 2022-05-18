using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint
{
    public Vector3 position;
    public int index;

    public Waypoint(Vector3 position, int index)
    {
        this.position = position;
        this.index = index;
    }
}

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] [Range(0, 2f)] float gizmoRadius = 0.5f;

    public Waypoint GetClosestWaypointFrom(Vector3 position)
    {
        Waypoint closestWaypoint = new Waypoint(transform.position, 0); // If PatrolRoute has no children, return value defaults to its own transform
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < transform.childCount; i++)
        {
            float distance = (transform.GetChild(i).position - position).magnitude;
            if ((distance < shortestDistance))
            {
                shortestDistance = distance;
                closestWaypoint.position = GetPositionByIndex(i);
                closestWaypoint.index = i;
            }
        }
        return closestWaypoint;
    }

    public Waypoint GetWaypointByIndex(int index)
    {
        return new Waypoint(transform.GetChild(index).position, index);
    }

    public Vector3 GetPositionByIndex(int index)
    {
        return transform.GetChild(index).position;
    }

    public Waypoint GetWaypointAfter(Waypoint currentWaypoint)
    {
        if (currentWaypoint.index + 1 >= transform.childCount)
            return GetWaypointByIndex(0);
        else 
            return GetWaypointByIndex(currentWaypoint.index + 1);
    }   

    public void DisplayGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = GetPositionByIndex(i);
            Gizmos.DrawSphere(pos, gizmoRadius);
            
            if (i+1 < transform.childCount)
                Gizmos.DrawLine(pos, GetPositionByIndex(i+1));
            else   
                Gizmos.DrawLine(pos, GetPositionByIndex(0));
        }

    }

    void OnDrawGizmosSelected()
    {
        DisplayGizmos();
    }
}
