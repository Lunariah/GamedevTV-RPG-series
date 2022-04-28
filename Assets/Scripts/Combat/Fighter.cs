using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Movement))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float attackRange = 1;
        Movement movement;
        void Awake()
        {
            movement = GetComponent<Movement>();
        }
        public void Attack(Target target)
        {
            Debug.Log("Yaa!");
            movement.GetInRangeFrom(target.transform.position, attackRange);
        }
    }
}
