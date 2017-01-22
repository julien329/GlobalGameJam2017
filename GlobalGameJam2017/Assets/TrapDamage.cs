using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour {

	public int damage = 20;
	public float coolDown = 0.5f;
	private bool isCoolDown = false;

	void OnCollisionEnter(Collision other) {
		
		if (other.transform.root.gameObject.CompareTag ("Player") && !isCoolDown) {

            // Apply Damage
            other.transform.root.gameObject.GetComponent<PlayerCombat>().ApplyDamage(damage);
            other.transform.root.gameObject.GetComponent<PlayerCombat>().ApplyImpulse(transform.position - other.transform.root.gameObject.transform.position, 50f);
            // Star Cooldown
            isCoolDown = true;
			StartCoroutine ("coolingDown");
		}
	}

	IEnumerator coolingDown(){
		// Cooldown
		yield return new WaitForSeconds (coolDown);
		// Cooldown ending
		isCoolDown = false;
	}
}
