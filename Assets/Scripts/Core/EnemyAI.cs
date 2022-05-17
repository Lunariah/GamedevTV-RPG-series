using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using UnityEngine.AI;

namespace RPG.Core
{
    [RequireComponent(typeof(Fighter))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] Vector3 guardPosition;
        [SerializeField] float chaseDistance = 0f;
        [SerializeField] float suspiciousPhaseTime = 3f;

        private Fighter fighter;
        private Movement movement;
        private Health health;
        private Health playerHealth;

        private float timeSinceLastSawPlayer = 0f;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            movement = GetComponent<Movement>();
            FindPlayer();

            
            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead) return;

            if (playerHealth == null && FindPlayer() == false)
            {
                Debug.LogError("No object tagged Player with a Health component could be found");
                return;
            }

            timeSinceLastSawPlayer += Time.deltaTime;


            if (InChaseRangeFromPlayer())
            {
                timeSinceLastSawPlayer = 0f;
                fighter.Attack(playerHealth);
            }
            else if (timeSinceLastSawPlayer < suspiciousPhaseTime)
            {
                fighter.CancelAttacking();
            }
            else
            {
                movement.MoveTo(guardPosition);
            }

            // Deprecated
            // if (guardPosition != null && movement.IsStopped())
            //     movement.MoveTo(guardPosition);
        }

        private bool FindPlayer()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            if (playerObject == null)
                return false;
            else
            {
                playerHealth = playerObject.GetComponent<Health>();
                return playerHealth != null;
            }
        }

        private bool InChaseRangeFromPlayer()
        {
            float distanceFromPlayer = (transform.position - playerHealth.transform.position).magnitude;
            return distanceFromPlayer <= chaseDistance;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
