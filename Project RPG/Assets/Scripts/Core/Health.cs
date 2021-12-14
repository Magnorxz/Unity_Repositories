using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        Animator animator;
        bool isDead = false;
        [SerializeField] float healthPoints = 100f;


        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            animator = GetComponent<Animator>();

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead)
            {
                animator.SetTrigger("Die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
                isDead = true;
            }

        }

    }
}


