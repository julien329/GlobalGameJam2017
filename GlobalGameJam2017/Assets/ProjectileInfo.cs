using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ProjectileInfo : MonoBehaviour
{

    private GameObject player;
    public float damage = 5f;
    public float lifeSteal = 0f;


    void Start()
    {

        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Enemy"))
        {
            IEnemy enemy = collider.GetComponent<IEnemy>();
            enemy.TakeDamage(5);
            Debug.Log(collider.gameObject);
        }
   
    }
}