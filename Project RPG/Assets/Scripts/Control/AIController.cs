using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine.AI;

namespace RPG.Control
{


    public class AIController : MonoBehaviour
    {


        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        Health health;
        Mover mover;
        Fighter fighter;
        GameObject player;
        NavMeshAgent navMeshAgent;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAIAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                navMeshAgent.speed = 3.5f;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                //Donde está el jugador?
                SuspiciousBehaviour();
            }
            else
            {
                navMeshAgent.speed = 1.5f;
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;

        }

        //La zona donde la Ai esta patrullando
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //Si el jugador sale de la zona de caza de la AI se quedarán parados un momento
        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        //Si el jugador entra dentro de la zona de patrulla de la AI,
        //esta automaticamente atacará al jugador
        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Llamado por unity para visualizar gizmos
        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.blue;
        //     Gizmos.DrawWireSphere(transform.position, chaseDistance);
        // }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 0.0f, 1));
            Gizmos.DrawWireSphere(Vector3.zero, chaseDistance);
            Gizmos.matrix = oldMatrix;
        }
    }
}
