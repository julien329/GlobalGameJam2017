using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOverTime : MonoBehaviour {

	private PlayerCombat player;
	public int HealingPerSecond = 5;
	public float duration = 3f;
    public float slowFactor = 2f;
    private PlayerMovement pm;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerCombat>();
		Destroy(gameObject, duration);
        pm= player.GetComponent<PlayerMovement>();
	    pm.speed = pm.speed/ slowFactor;
	    StartCoroutine("healingOverTime");
	}
	


    void OnDestroy()
    {
        pm.speed=pm.speed* slowFactor;
    }

    IEnumerator healingOverTime()
    {
        for (int i = 0; i < duration; i++)
        {
            player.RestoreHealth(HealingPerSecond);

            yield return new WaitForSeconds(1);
        }
    }
}
