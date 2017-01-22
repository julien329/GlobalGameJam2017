using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    GameObject panel;

	// Use this for initialization
	void Start () {
        panel = transform.GetChild(0).gameObject;
	}

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        //transform.GetChild(0).rotation = Quaternion.LookRotation(Camera.main.transform.position - panel.transform.position);
    }

    public void UpdateBar(int HpMax,int Hp)
    {
        if (Hp < 0)
            Hp = 0;
        gameObject.transform.localScale = new Vector3((float)Hp / HpMax, 1, 1);
    }
}
