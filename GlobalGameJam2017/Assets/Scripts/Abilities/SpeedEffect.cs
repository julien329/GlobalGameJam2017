using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private PlayerCombat player;
    private PlayerMovement pm;
    private UIButtonBufferManager buttonBuffer;
    public float duration = 3f;
    public float fastFactor = 2f;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        buttonBuffer = GameObject.Find("Tab").GetComponent<UIButtonBufferManager>();
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        pm = player.GetComponent<PlayerMovement>();
    }


    void Start() {
        buttonBuffer.SetSpeedDisabled();
        Destroy(gameObject, duration);
        pm.speed = pm.speed * fastFactor;
    }


    void OnDestroy() {
        pm.speed = pm.speed / fastFactor;
    }

}
