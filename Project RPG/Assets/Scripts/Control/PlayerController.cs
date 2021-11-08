using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveToCursor();
            }
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();

            }
  
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }

        }


    }
}


//Codigo para que el jugador parase de moverse una vez sueltes el click izquierdo
//Pero no funciona como me gustaría por lo que lo dejo comentado por ahora

// if (Input.GetMouseButton(0))
// {
//     holdTimer += Time.deltaTime;
//     Debug.Log(holdTimer);
//     if (holdTimer > 1)
//     {
//         wasHolding = true;
//     }
//     MoveToCursor();

// }

// if (Input.GetMouseButtonUp(0) && wasHolding)
// {
//     holdTimer = 0f;
//     //Con esto puedo añadir un temporizador(en este caso de 1 segundo)
//     //para parar al personaje si suelto el ratón al pasar 1 segundo, despues de haberlo mantenido
//     //esto no afecta a si solo hago un click
//     StartCoroutine(Break());
//     wasHolding = false;
// }

// private IEnumerator Break()
// {
//     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//     RaycastHit hit;
//     bool hasHit = Physics.Raycast(ray, out hit);
//     if (hasHit)
//     {
//         yield return new WaitForSecondsRealtime(0.25f);
//         GetComponent<Mover>().MoveTo(target.transform.position);
//         // GetComponent<NavMeshAgent>().destination = Vector3.Lerp(target.transform.position, move, Time.time);
//     }



// }