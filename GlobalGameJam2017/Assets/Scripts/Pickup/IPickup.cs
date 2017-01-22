using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickup : MonoBehaviour {
    [SerializeField]
    protected GameObject pickupEffect;
    [SerializeField]
    protected GameObject expireEffect;
    [SerializeField]
    protected GameObject appearEffect;

    //Time the pickup stays on the ground
    public float lifetime = 15.0f;

	// Use this for initialization
	void Start () {
        var exp = Instantiate(appearEffect, transform.position, transform.rotation);
        Destroy(exp, 5.0f);
        StartCoroutine("FadeTimer");
	}

    //Destroys the item after a certain time
    IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(lifetime);
        if(gameObject != null)
        {
            var exp = Instantiate(expireEffect, transform.position, transform.rotation);
            Destroy(exp, 5.0f);
            Destroy(gameObject);
        }
    }

    //Action is played when item is picked up
    protected abstract void PickupAction(GameObject player);

}
