using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float fistOneDamage = 1f;
        [SerializeField] float fistTwoDamage = 2f;
        [SerializeField] float lastFistDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0;

        Animator animator;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
                if (target == null)
                {
                    animator.ResetTrigger("Attack 1");
                }

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        //Habilita la animacion de ataque
        private void AttackBehaviour()
        {

            AttackAnimation(animator);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {

                timeSinceLastAttack = 0;
                // Health healthComponent = target.GetComponent<Health>();

                // healthComponent.TakeDamage(weaponDamage);
            }

        }

        private void AttackAnimation(Animator animator)
        {

            animator = GetComponent<Animator>();

            animator.SetTrigger("Attack 1");

            if (isPlaying(animator, "Attack 1"))
            {
                animator.SetTrigger("Attack 2");

            }
            if (isPlaying(animator, "Attack 2"))
            {
                animator.SetTrigger("Attack 3");

            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;

        }

        //AnimationEvent Ignore!!!
        void Hit(float damage)
        {
            animator = GetComponent<Animator>();

            Health healthComponent = target.GetComponent<Health>();
            if (isPlaying(animator, "Attack 1"))
            {
                healthComponent.TakeDamage(fistOneDamage);
            }
            if (isPlaying(animator, "Attack 2"))
            {
                healthComponent.TakeDamage(fistTwoDamage);
            }
            if (isPlaying(animator, "Attack 3"))
            {
                healthComponent.TakeDamage(lastFistDamage);
            }
        }

        //Returns if a trigger is set or not
        bool isPlaying(Animator anim, string stateName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
                return true;
            else
                return false;
        }
    }
}

