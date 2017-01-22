using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ProjectileInfo : MonoBehaviour
{

    private GameObject player;
    public float damage = 0;
    public float lifeSteal = 0f;
    public float scale = 0f;

    void Start()
    {

        player = GameObject.Find("Player");
    }
    //
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + scale * Time.deltaTime, transform.localScale.y, transform.localScale.z);
    }


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Enemy"))
        {
            IEnemy enemy = collider.GetComponent<IEnemy>();
            enemy.TakeDamage((int)damage);
            if (lifeSteal > 0)
            {
                player.GetComponent<PlayerCombat>().RestoreHealth((int)lifeSteal);
            }
            Debug.Log(collider.gameObject);

            // Projectil destruction
            Destroy(this.gameObject);
        }
   
    }
}