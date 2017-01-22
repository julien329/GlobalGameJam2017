using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOverTime : MonoBehaviour {

	private PlayerCombat player;
	public float HealingPerSecond = 5f;
	public float duration = 3f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerCombat>();
		Destroy(gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		player.RestoreHealth((int)(HealingPerSecond * Time.deltaTime));
	}
}
