using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Fighter))]
public class PlayerInput : MonoBehaviour
{
    private Movement movement; 
    private Fighter fighter;
    new private Camera camera;
    
    void Awake()
    {
        movement = GetComponent<Movement>();
        fighter = GetComponent<Fighter>();
        camera = Camera.main; // Do it properly later
        
        // if (camera == null)
        // {
        //     camera = Camera.main;
        //     if (camera == null)
        //     {
        //         Debug.Log("PlayerMovement.cs: Can’t find camera");
        //         this.enabled = false;
        //     }
        // }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }

    void MoveToCursor()
    {
        Ray movementRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(movementRay, out hit); // Switch to RaycastAll later
        
        if (hasHit)
        {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                fighter.Attack(target);
                return;
            }
            movement.MoveTo(hit.point);
        }
    }
}
