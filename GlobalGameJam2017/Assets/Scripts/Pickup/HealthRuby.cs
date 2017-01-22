using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRuby : IPickup
{ 

    protected override void PickupAction(GameObject player)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(GameObject.Find("Player").transform.position, enemy.transform.position);
            enemy.GetComponent<IEnemy>().TakeDamage(UnityEngine.Random.Range(1, 3));
            
        }
        var ring = Instantiate(pickupEffect, player.transform.localPosition, Quaternion.LookRotation(Vector3.up, Vector3.right), player.transform);
        Destroy(ring, 5.0f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.name == "Player")
        {
            PickupAction(coll.gameObject);
        }
    }
}
