using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Combat;

namespace RPG.Core
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Fighter))]
    [SelectionBase]
    public class PlayerInput : MonoBehaviour
    {
        private GameMode controlMode;
        private Movement movement; 
        private Fighter fighter;
        private Health health;
        new private Camera camera;

        public enum GameMode
        {
            Combat,
            Dialogue,
            Cinematic,
            Menu
        }

        public void EnterCinematic(PlayableDirector director, bool cancelMovement=true/*, GameMode exitMode=GameMode.Combat */) 
        // Todo: Wire in states to avoid cinematics overlapping
        {
            controlMode = GameMode.Cinematic;
            fighter.CancelAttacking();
            if (cancelMovement) movement.StayPut();
            
            director.stopped += ExitCinematic;
            director.Play();
        }

        void ExitCinematic(PlayableDirector dummy /*, GameMode exitMode*/)
        {
            controlMode = GameMode.Combat;
        }
        
        void Awake()
        {
            controlMode = GameMode.Combat; // Temporary


            movement = GetComponent<Movement>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
            switch (controlMode)
            {
                case GameMode.Combat:
                    if (Input.GetMouseButton(0))
                    {
                        RaycastClick();
                    }
                    break;
                default:
                    break;
            }
        }

        void RaycastClick()
        {
            if (health.IsDead) return;

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
}

