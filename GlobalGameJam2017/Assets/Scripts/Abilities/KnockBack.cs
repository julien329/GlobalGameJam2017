using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private GameObject[] enemies;
    private UIButtonBufferManager buttonBuffer;
    public float force;
    public float radius;
    public float upwardsModifier;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        buttonBuffer = GameObject.Find("Tab").GetComponent<UIButtonBufferManager>();
    }


    void Start () {
        buttonBuffer.SetRadiusDisabled();
	    enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(GameObject.Find("Player").transform.position, enemy.transform.position);
            if (distance < 8.0f) {
                enemy.GetComponent<IEnemy>().ShockwaveHit(distance);
            }
            //enemy.GetComponent<Rigidbody>().MovePosition(Vector3.forward * 150);
            //enemy.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, ForceMode.Impulse);
        }
    }
}
