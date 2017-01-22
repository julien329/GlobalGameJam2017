using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour {

	public int damage = 20;
	public float coolDown = 0.5f;
	private bool isCoolDown = false;

	void OnTriggerStay(Collider other) {
		
		if (other.gameObject.CompareTag ("Player") && !isCoolDown) {

			// Apply Damage
			other.gameObject.GetComponent<PlayerCombat> ().ApplyDamage (damage);

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
