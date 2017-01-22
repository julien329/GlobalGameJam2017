﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private Transform player;
    private Vector3 offset;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        player = GameObject.Find("Player").transform;
    }


    void Start () {
        offset = transform.position - player.transform.position;
	}
	

	void Update () {
        if (player) {
            transform.position = player.position + offset;
        }
	}
}
