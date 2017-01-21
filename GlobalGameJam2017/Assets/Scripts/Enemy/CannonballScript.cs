using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour {
    [SerializeField]
    GameObject explosion;
    Rigidbody rb;
    public float force;
    public float liftFactor;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 target = GameObject.Find("Player").transform.position;

        //Set force depending on target distance
        float distance = Vector3.Distance(target, transform.position);

        if (distance < 8.0f)
            force = 100;
        else if (distance < 10.0f)
            force = 120;
        else if (distance < 12.0f)
            force = 140;
        else if (distance < 15.0f)
            force = 160;
        else
            force = 180;

        rb.AddExplosionForce(force, transform.position - transform.forward, 3.0f, liftFactor, ForceMode.Impulse);
	}
	
	void OnCollisionEnter(Collision coll)
    {
        var exp = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(exp, 5.0f);
        Destroy(gameObject);
    }
}
