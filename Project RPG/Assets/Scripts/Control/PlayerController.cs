using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

        }

    
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);

                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
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