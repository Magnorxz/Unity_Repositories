using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    bool wasHolding;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !wasHolding)
        {
            MoveToCursor();
        }
        if (Input.GetMouseButton(0))
        {
            GetComponent<NavMeshAgent>().speed = 5.66f;
            MoveToCursor();
            wasHolding = true;
        }
        if (Input.GetMouseButtonUp(0) && wasHolding)
        {
            GetComponent<NavMeshAgent>().speed = 0;
            wasHolding = false;
        }
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

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }

    }
}
