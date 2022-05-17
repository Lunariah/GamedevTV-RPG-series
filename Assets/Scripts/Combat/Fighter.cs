using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Movement))]
    //[RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float attackRange = 1;
        [SerializeField] float attackDelay = 0.8f;
        [SerializeField] float attackPower = 1f;
        float remainingDelay = 0;
        Movement movement;
        Animator animator;
        [SerializeField] Health target;


        void Awake()
        {
            movement = GetComponent<Movement>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target == null)
                return;

            bool inRange;
            movement.GetInRangeFrom(target.transform.position, attackRange, out inRange);
            remainingDelay -= Time.deltaTime;
            if (inRange)
            {
                movement.RotateTowards(target.transform, 1);
                if (remainingDelay <= 0)
                {
                    remainingDelay = attackDelay;
                    animator.SetTrigger("Attack");
                }
            }
        }
        public void Attack(Health newTarget)
        {
            if (newTarget.IsDead)
                return;

            target = newTarget;
        }

        public void CancelAttacking()
        {
            target = null;
            animator.SetTrigger("CancelAttack");
        }

        void Hit() // Animation event
        {
            if (target == null)
                return;

            target.TakeDamage(attackPower);
            if (target.IsDead)
            {
                target = null;
            }
        }
    }
}
