using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement

{
    public class Mover : MonoBehaviour
    {

        Vector3 move;

        void Update()
        {

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            //Convierte la velocidad de global a local, haciendo saber al animador si el personaje
            //se esta moviendo hacia delante y a que velocidad, en vez de la posicion en el mundo
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            //Inyecta la velocidad recogida anteriormente en el animador, transformando al personaje
            //dependiendo de la velocidad en la que se este moviendo
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }



        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }



    }
}

