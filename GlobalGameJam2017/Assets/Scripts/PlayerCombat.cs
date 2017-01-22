using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    int HP;
    
	// Use this for initialization
	void Start () {
        HP = 100;
	}

    public void ApplyDamage(int damage)
    {
        HP -= damage;
        if(HP < 1)
        {
            PlayerDies();
        }
    }

    public void PlayerDies()
    {

    }

    public void ApplyImpulse(Vector3 direction, float power)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }

}
