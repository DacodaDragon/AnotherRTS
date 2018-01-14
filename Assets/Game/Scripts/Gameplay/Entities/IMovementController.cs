using UnityEngine;

namespace AnotherRTS.Gameplay.Entities
{
    public interface IMovementController
    {
        void MoveTowards(Vector3 target);
        void MoveTowards(Transform target);
        void Follow(Transform target);
        bool HasReachedTarget();
    }
}