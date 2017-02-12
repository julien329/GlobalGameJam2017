using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour {
    [SerializeField]
    GameObject explosion;
    Rigidbody rb;
    GameObject player;
    public float force;
    public float liftFactor;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        Vector3 target = player.transform.position + player.GetComponent<PlayerMovement>().Velocity * 2;

        //Set force depending on target distance
        float distance = Vector3.Distance(target, transform.position);

        if (distance < 8.0f)
            force = 120;
        else if (distance < 10.0f)
            force = 140;
        else if (distance < 12.0f)
            force = 150;
        else if (distance < 15.0f)
            force = 160;
        else
            force = 180;

        rb.AddForce(Vector3.Normalize(transform.forward) * force + new Vector3(0,30f,0), ForceMode.Impulse);
	}
	
	void OnCollisionEnter(Collision coll) {
        var exp = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
        if (player) {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < 2.5f) {
                Debug.Log("It's a hit!");
                if (distance < 1.0f)
                    player.GetComponent<PlayerCombat>().ApplyDamage(25);
                else
                    player.GetComponent<PlayerCombat>().ApplyDamage(10);
                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.AddExplosionForce(12f, transform.position, 5.0f, 4f, ForceMode.Impulse);
            }
        }

        Destroy(exp, 5.0f);
        Destroy(gameObject);
    }
}
