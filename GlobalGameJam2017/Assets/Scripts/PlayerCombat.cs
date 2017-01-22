using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] shortHurtSounds;
    public AudioClip[] longHurtSounds;
    public HealthKnobManager healthKnob;
    public float shieldWeight = 20f;
    public float baseWeight = 1f;

    [SerializeField]
    private GameObject deathExplosion;
    [SerializeField]
    private GameObject shield;
    private AudioSource audioSource;
    private Rigidbody playerRigidbody;
    private int HP;
    private int buffTimer;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


	void Start () {
        HP = 100;
        InvokeRepeating("DecrementTimer", 1.0f, 1.0f);
	}


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    /////////////////////////////////////////////////////////////////////////////////////////////////

    public void ApplyDamage(int damage) {
        HP -= damage;
        if(HP < 1) {
            PlayerDies();
        }
        else {
            audioSource.clip = shortHurtSounds[Random.Range(0, shortHurtSounds.Length)];
            audioSource.Play();
        }

        //Modifies health bar
        if (healthKnob) {
            healthKnob.damage((float)damage, 100);
        }
    }


    public void PlayerDies() {
        audioSource.clip = longHurtSounds[Random.Range(0, longHurtSounds.Length)];
        audioSource.Play();
		var exp = Instantiate(deathExplosion, gameObject.transform.position, Quaternion.Euler(-90,0,0));
        Destroy(exp, 5.0f);
    }


    public void RestoreHealth(int ammount) {
        HP += ammount;
        if (HP > 100)
            HP = 100;

        //Modifies health bar
        if (healthKnob) {
            healthKnob.heal((float)ammount, 100);
        }
    }


    public void ApplyImpulse(Vector3 direction, float power) {
        playerRigidbody.AddForce(direction * power, ForceMode.Impulse);
    }


    public void ApplyShield() {
        playerRigidbody.mass = shieldWeight;
        shield.SetActive(true);
        if (buffTimer < 5)
            buffTimer = 15;
        else
            buffTimer += 10;
    }


    public void DecrementTimer() {
        if (buffTimer > 0) {

            buffTimer--;
            if (buffTimer <= 0)
                EndBuff();
        }
    }


    void EndBuff() {
        playerRigidbody.mass = baseWeight;
        shield.SetActive(false);
    }

}
