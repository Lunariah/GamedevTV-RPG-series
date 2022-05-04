using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Fighter))]
[SelectionBase]
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
            Target validTarget = hit.collider.GetComponent<Target>();
            if (validTarget != null)
            {
                fighter.Attack(validTarget);
            }
            else
            {
                fighter.CancelAttacking();
                movement.MoveTo(hit.point);
            }
        }
    }
}
