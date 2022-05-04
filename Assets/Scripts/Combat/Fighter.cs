using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Movement))]
    //[RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float attackRange = 1;
        [SerializeField] float attackDelay = 0.8f;
        float remainingDelay = 0;
        Movement movement;
        Animator animator;
        [SerializeField] Target target;


        void Awake()
        {
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target != null)
            {
                bool inRange;
                movement.GetInRangeFrom(target.transform.position, attackRange, out inRange);
                Debug.Log(inRange);
                remainingDelay -= Time.deltaTime;
                if (inRange)
                {
                    if (remainingDelay <= 0)
                    {
                        remainingDelay = attackDelay;
                        animator.SetTrigger("Attack");
                    }
                }
            }
        }
        public void Attack(Target newTarget)
        {
            target = newTarget;
        }

        public void CancelAttacking()
        {
            target = null;
        }

        void Hit() // Animation event
        {
            Debug.Log("Take that!");
        }
    }
}
