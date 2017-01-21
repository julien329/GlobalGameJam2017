using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour {

    Rigidbody rb;
    public float force;
    public float liftFactor;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position - Vector3.forward, 5.0f, liftFactor, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
