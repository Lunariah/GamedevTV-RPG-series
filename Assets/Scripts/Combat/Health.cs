using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 1f;
        Animator animator;

        public bool IsDead {get; private set;}

        void Awake()
        {
            IsDead = false;
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            if (damage >= health)
            {
                health = 0;
                Die();
            }
            else
            {
                health -= damage;
            }
        }

        private void Die()
        {
            if (IsDead) return;
            if (animator != null)
            {
                IsDead = true;
                animator.SetTrigger("Die");
            }
        }
    }
}
