using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickup : MonoBehaviour {

    //Time the pickup stays on the ground
    public float lifetime = 15.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine("FadeTimer");
	}

    //Destroys the item after a certain time
    IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(lifetime);
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    //Action is played when item is picked up
    protected abstract void PickupAction(GameObject player);

}
