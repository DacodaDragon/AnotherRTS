using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherRTS.Gameplay.Entities;
using AnotherRTS.Gameplay.Entities.Units;
using UnityEngine.AI;
using AnotherRTS.Management.RemappableInput;


public class UnitPlacer : MonoBehaviour
{
    [SerializeField] GameObject UnitPrefab;
    int m_key;
    InputManager inputmanager;
    // Update is called once per frame
    public void Start()
    {
        inputmanager = InputManager.Instance;
        m_key = inputmanager.GetKeyID("single place");
    }

	void Update () {
		if (inputmanager.GetKey(m_key))
        {
            RaycastHit hitInfo;
            bool hitSuccess = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Camera.main.farClipPlane);

            if (hitSuccess)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(hitInfo.point, out hit, 1, NavMesh.AllAreas))
                {
                    GameObject obj = Instantiate(UnitPrefab, hit.position, Quaternion.identity);
                    UnitManager.Instance.AddUnits(obj.GetComponent<Unit>());
                }
            }
        }
	}
}
