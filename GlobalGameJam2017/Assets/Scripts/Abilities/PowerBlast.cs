using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlast : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private UIButtonBufferManager buttonBuffer;
    private float timeSpent;
    private int step;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        buttonBuffer = GameObject.Find("Tab").GetComponent<UIButtonBufferManager>();
    }


    void Start () {
        buttonBuffer.setPowerDisabled();
        Destroy(gameObject, 5.0f);
        timeSpent = 0.0f;

	}

	
	void Update () {
        timeSpent += Time.deltaTime;
        switch (step) {
            case 0:
                if(timeSpent >=0.5f) {
                    transform.GetChild(1).gameObject.GetComponent<CapsuleCollider>().enabled = true;
                    step++;
                }
                break;
            case 1:
                if (timeSpent >= 1.0f) {
                    transform.GetChild(2).gameObject.GetComponent<CapsuleCollider>().enabled = true;
                    step++;
                }
                break;
            case 2:
                if (timeSpent >= 2.0f) {
                    transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    step++;
                }
                break;
            case 3:
                if (timeSpent >= 2.5f) {
                    transform.GetChild(1).gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    step++;
                }
                break;
            case 4:
                if (timeSpent >= 3.0f) {
                    transform.GetChild(2).gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    step++;
                }
                break;
            default:
                break;
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.transform.root.CompareTag("Enemy"))
            other.transform.root.gameObject.GetComponent<IEnemy>().TakeDamage(5);
    }
}
