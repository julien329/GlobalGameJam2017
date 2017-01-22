using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOverTime : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;
    private UIButtonBufferManager buttonBuffer;
    public int HealingPerSecond = 5;
	public float duration = 3f;
    public float slowFactor = 2f;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        buttonBuffer = GameObject.Find("Tab").GetComponent<UIButtonBufferManager>();
        playerMovement = playerCombat.GetComponent<PlayerMovement>();
    }


    void Start () {
        buttonBuffer.SetHealingDisabled();
        Destroy(gameObject, duration);
        playerMovement.speed = playerMovement.speed / slowFactor;
	    StartCoroutine("healingOverTime");
	}


    void OnDestroy() { 
        playerMovement.speed = playerMovement.speed * slowFactor;
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    /////////////////////////////////////////////////////////////////////////////////////////////////

    IEnumerator healingOverTime() {
        for (int i = 0; i < duration; i++) {
            playerCombat.RestoreHealth(HealingPerSecond);
            yield return new WaitForSeconds(1);
        }
    }
}
