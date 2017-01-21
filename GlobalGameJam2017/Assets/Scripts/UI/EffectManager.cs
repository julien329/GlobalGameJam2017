using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour {

    public int counter;
    public int limit;
    public float growth;
    Vector3 initSize;

	// Use this for initialization
	void Start () {
        counter = 0;
        limit = 12;
        growth = 1.04f;
        initSize = this.transform.localScale;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (this.GetComponent<Image>().enabled == true && counter < limit)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * growth, this.transform.localScale.y * growth, this.transform.localScale.z);
            counter++;
        }

        if (counter >= limit)
        {
            counter = 0;
            this.GetComponent<Image>().enabled = false;
            this.transform.localScale = initSize;
        }
	}
}
