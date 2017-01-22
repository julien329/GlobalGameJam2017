using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] shortHurtSounds;
    public float shieldWeight = 20f;
    public float baseWeight = 1f;

    [SerializeField]
    private GameObject deathExplosion;
    [SerializeField]
    private GameObject shield;
    private AudioSource audioSource;
    private Rigidbody playerRigidbody;
    private HealthKnobManager healthKnob;
    private GameFlow gameflow;
    private int HP;
    private int buffTimer;
    private HitController hitController;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        healthKnob = GameObject.Find("HealthKnob").GetComponent<HealthKnobManager>();
        gameflow = GameObject.Find("Map").GetComponent<GameFlow>();
        hitController=GetComponent<HitController>();
    }


	void Start () {
        HP = 100;
        InvokeRepeating("DecrementTimer", 1.0f, 1.0f);
	}


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    /////////////////////////////////////////////////////////////////////////////////////////////////

    public void ApplyDamage(int damage) {
        if (buffTimer > 0)
            damage /= 2;
        HP -= damage;

        //Show hit text
        hitController.color = Color.red;
        hitController.createHitText(damage,transform);

        //Warn achievements that we have failed.
        ScoreHandler.DamageTaken();
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
        healthKnob.damage(100, 100);
        gameflow.PlayerDied();
		var exp = Instantiate(deathExplosion, transform.position, Quaternion.Euler(-90,0,0));
        Destroy(exp, 5.0f);
        Destroy(gameObject);
    }


    public void RestoreHealth(int ammount) {
        HP += ammount;
        if (HP > 100) {
            HP = 100;
        }
        //Show heal text
        hitController.color=Color.green;
        hitController.createHitText(ammount, transform);

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
        if (buffTimer < 5) {
            buffTimer = 15;
        }
        else {
            buffTimer += 10;
        }
    }


    public void DecrementTimer() {
        if (buffTimer > 0) {
            buffTimer--;
            if (buffTimer <= 0) {
                EndBuff();
            }
        }
    }


    void EndBuff() {
        playerRigidbody.mass = baseWeight;
        shield.SetActive(false);
    }
}
