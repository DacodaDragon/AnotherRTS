using System.Collections;
using AnotherRTS.Management.RemappableInput;
using UnityEngine;

public class InputTester : MonoBehaviour {

    [SerializeField]
    string name;
    int id;

    [SerializeField]
    InputManager manager;

	// Use this for initialization
	void Start () {
        id = manager.GetKeyID(name);
        Debug.Log("ID is : " + id);
	}
	
	// Update is called once per frame
	void Update () {
        if (manager.GetKeyDown(id))
            Debug.Log("Pressed!");
        if (manager.GetKey(id))
            Debug.Log("Held!");
        if (manager.GetKeyUp(id))
            Debug.Log("Released!");
    }
}
