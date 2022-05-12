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
            RaycastClick();
        }
    }

    void RaycastClick()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] interactableHits = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.GetMask("Interactable"));

        foreach (RaycastHit hit in interactableHits)
        {
            Health validTarget = hit.collider.GetComponent<Health>();

            // If a valid target is hit
            if (validTarget != null && !validTarget.IsDead)
            {
                fighter.Attack(validTarget);
                return;
            } 
        }

        // If no interactable is found, process movement
        RaycastHit walkableHit; 
        if (Physics.Raycast(ray, out walkableHit, LayerMask.GetMask("Walkable")))
        {
            fighter.CancelAttacking();
            movement.MoveTo(walkableHit.point);
        }
    }
}
