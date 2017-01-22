using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] shortHurtSounds;
    public AudioClip[] longHurtSounds;

    private AudioSource audioSource;
    private int HP;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }


	void Start () {
        HP = 100;
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
    }


    public void RestoreHealth(int ammount) {
        HP += ammount;
        if (HP > 100)
            HP = 100;
    }


    public void ApplyImpulse(Vector3 direction, float power) {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }

}
