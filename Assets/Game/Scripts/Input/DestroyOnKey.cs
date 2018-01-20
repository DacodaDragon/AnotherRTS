using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnKey : MonoBehaviour {
	void Update () {
        if (Input.GetKeyUp(KeyCode.I))
            Destroy(gameObject);
	}
}
