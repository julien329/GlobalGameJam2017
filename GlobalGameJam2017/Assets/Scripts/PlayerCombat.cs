using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    int HP;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyImpulse(Vector3 direction, float power)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }

}
