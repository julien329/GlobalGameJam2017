using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{

    private PlayerCombat player;
    public int HealingPerSecond = 5;
    public float duration = 3f;
    public float fastFactor = 2f;
    private PlayerMovement pm;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        Destroy(gameObject, duration);
        pm = player.GetComponent<PlayerMovement>();
        pm.speed = pm.speed * fastFactor;
    }



    void OnDestroy()
    {
        pm.speed = pm.speed / fastFactor;
    }

}
