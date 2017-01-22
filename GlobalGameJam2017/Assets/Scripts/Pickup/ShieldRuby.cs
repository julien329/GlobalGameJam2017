using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRuby : IPickup
{

    protected override void PickupAction(GameObject player)
    {
        //player.GetComponent<PlayerCombat>().RestoreHealth(20);
        var ring = Instantiate(pickupEffect, player.transform.localPosition, Quaternion.LookRotation(Vector3.up, Vector3.right), player.transform);
        Destroy(ring, 5.0f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Player")
        {
            PickupAction(coll.gameObject);
        }
    }
}
