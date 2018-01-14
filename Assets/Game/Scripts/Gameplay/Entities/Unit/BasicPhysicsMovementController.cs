using System;
using UnityEngine;
using UnityEngine.AI;

namespace AnotherRTS.Gameplay.Entities.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicPhysicsMovementController : MonoBehaviour, IMovementController
    {
        NavMeshAgent aStar;

        public void Awake()
        {
            aStar = GetComponent<NavMeshAgent>();
        }

        public void Follow(Transform target)
        {
            throw new NotImplementedException();
        }

        public void MoveTowards(Vector3 target)
        {
            aStar.SetDestination(target);

        }

        public void MoveTowards(Transform target)
        {
            aStar.SetDestination(target.position);

        }
    }
}

