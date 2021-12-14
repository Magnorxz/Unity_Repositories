using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float fistOneDamage = 1f;
        [SerializeField] float fistTwoDamage = 2f;
        [SerializeField] float lastFistDamage = 4f;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        Animator animator;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead())
            {
                Cancel();
            }
            if (!GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void ResetTriggers()
        {
            try
            {
                animator.ResetTrigger("Attack 1");
                animator.ResetTrigger("Attack 2");
                animator.ResetTrigger("Attack 3");
                
            }
            catch (NullReferenceException)
            {

            }

        }

        //Habilita la animacion de ataque
        private void AttackBehaviour()
        {
            AttackAnimation(animator);


            //Ahora mismo no sirve para nada, porque no controlo bien el animador
            //el resultado ahora mismo es el que quiero, pero en el futuro podría necesitar
            //este codigo



            // if (timeSinceLastAttack > timeBetweenAttacks)
            // {
            //     AttackAnimation(animator);
            //     timeSinceLastAttack = 0;


            // }



        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null || combatTarget.tag == "Player")
            {
                return false;
            }
            // if(combatTarget.tag == "Player"){
            //     return false;
            // }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public bool CanAIAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            transform.LookAt(combatTarget.transform);
            target = combatTarget.GetComponent<Health>();
        }

        private void AttackAnimation(Animator animator)
        {

            animator = GetComponent<Animator>();

            //Aqui se llama a Hit()
            if (GetIsInRange())
            {
                animator.SetTrigger("Attack 1");
            }
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

            {
                return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            }

        }



        public void Cancel()
        {
            ResetTriggers();
            GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
        }

        //Cuando salta el evento de golpear de la animacion hace daño dependiendo
        //del ataque o arma
        void Hit(float damage)
        {
            animator = GetComponent<Animator>();

            if (target == null) return;
            if (isPlaying(animator, "Attack 1"))
            {
                target.TakeDamage(fistOneDamage);
            }
            if (isPlaying(animator, "Attack 2"))
            {
                target.TakeDamage(fistTwoDamage);
            }
            if (isPlaying(animator, "Attack 3"))
            {
                target.TakeDamage(lastFistDamage);
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



