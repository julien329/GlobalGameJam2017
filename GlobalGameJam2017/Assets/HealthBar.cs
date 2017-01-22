using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    GameObject panel;

	// Use this for initialization
	void Start () {
        panel = transform.GetChild(0).transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).transform.rotation = Camera.main.transform.rotation;
	}

    public void UpdateBar(int maxHP, int HP)
    {
        if (HP < 0) HP = 0;
        panel.transform.localScale = new Vector3((float)HP / maxHP, 1, 1);
        
    }
}
