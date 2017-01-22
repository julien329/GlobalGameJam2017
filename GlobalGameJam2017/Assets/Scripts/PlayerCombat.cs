using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    [SerializeField]
    GameObject deathExplosion;
    [SerializeField]
    GameObject shield;

    int HP;
    int buffTimer;
    public float shieldWeight = 20f;
    public float baseWeight = 1f;
    
	// Use this for initialization
	void Start () {
        HP = 100;
        InvokeRepeating("DecrementTimer", 1.0f, 1.0f);
	}

    public void ApplyDamage(int damage)
    {
        HP -= damage;
        if(HP < 1)
        {
            PlayerDies();
        }
    }

    public void PlayerDies()
    {
        var exp = Instantiate(deathExplosion, gameObject.transform.position, Quaternion.Euler(-90,0,0));
        Destroy(exp, 5.0f);
    }

    public void RestoreHealth(int ammount)
    {
        HP += ammount;
        if (HP > 100)
            HP = 100;
    }

    public void ApplyImpulse(Vector3 direction, float power)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }

    public void ApplyShield()
    {
        gameObject.GetComponent<Rigidbody>().mass = shieldWeight;
        shield.SetActive(true);
        if (buffTimer < 5)
            buffTimer = 15;
        else
            buffTimer += 10;
    }

    public void DecrementTimer()
    {
        if (buffTimer > 0)
        {

            buffTimer--;
            if (buffTimer <= 0)
                EndBuff();
        }

    }

    void EndBuff()
    {
        gameObject.GetComponent<Rigidbody>().mass = baseWeight;
        shield.SetActive(false);
    }

}
