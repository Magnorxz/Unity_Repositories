using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement

{
    public class Mover : MonoBehaviour, IAction
    {

        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }



        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            //Convierte la velocidad de global a local, haciendo saber al animador si el personaje
            //se esta moviendo hacia delante y a que velocidad, en vez de la posicion en el mundo
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            //Inyecta la velocidad recogida anteriormente en el animador, transformando al personaje
            //dependiendo de la velocidad en la que se este moviendo
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }



    }
}

