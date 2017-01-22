using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] shortHurtSounds;
    public AudioClip[] longHurtSounds;
    public float shieldWeight = 20f;
    public float baseWeight = 1f;

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

    [SerializeField]
    GameObject deathExplosion;

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
    }


    public void ApplyImpulse(Vector3 direction, float power) {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }


    public void ApplyShield() {
        ApplyDamage(100); //DEBUG

        gameObject.GetComponent<Rigidbody>().mass = shieldWeight;

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
    }
}
