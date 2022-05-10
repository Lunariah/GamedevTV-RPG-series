using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 1f;

        public void TakeDamage(float damage)
        {
            if (damage >= health)
            {
                health = 0;
                Die();
            }
            else
            {
                health -= damage;
                Debug.Log("Ow");
            }
        }

        private void Die()
        {
            Debug.Log("Am dead");
        }
    }
}
