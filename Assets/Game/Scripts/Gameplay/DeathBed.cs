using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AnotherRTS.Gameplay.Entities;

public class DeathBed : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        IDestroyable entity = other.GetComponent<IDestroyable>();

        if (entity != null)
        {
            entity.Destroy();
        }
    }
}
