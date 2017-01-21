using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDummy : MonoBehaviour {

    NavMeshAgent nm;

	// Use this for initialization
	void Start () {
        nm = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                nm.SetDestination(hit.point);

                // Do something with the object that was hit by the raycast.
            }
        }
	}
}
