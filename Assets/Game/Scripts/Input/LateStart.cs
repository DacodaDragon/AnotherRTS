using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LateStart : MonoBehaviour {

	void Start () {
        GetComponent<NavMeshAgent>().enabled = true;
        Destroy(this);
	}
}
