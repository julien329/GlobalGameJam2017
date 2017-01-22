using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] longHurtSounds;
    public float initSpawnTime;
    public float spawnTimeGrowth;
    public float initMaxNbEnemies;
    public float maxNbEnemiesGrowth;
    public float initNbMummy;
    public float nbMummyGrowth;
    public float initNbMinion;
    public float nbMinionGowth;
    public float initNbGolem;
    public float nbGolemGrowth;
    public float timeBetweenWaves;
    public float deathTimer;

    private AudioSource audioSource;
    private SpawnManager spawnManager;
    private float countDown;
    private float spawnTime;
    private float maxNbEnemies;
    private float nbMummy;
    private float nbMinion;
    private float nbGolem;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake () {
        audioSource = GetComponent<AudioSource>();
        spawnManager = GetComponent<SpawnManager>();
    }


    void Start() {
        spawnTime = initSpawnTime;
        maxNbEnemies = initMaxNbEnemies;
        nbMummy = initNbMummy;
        nbMinion = initNbMinion;
        nbGolem = initNbGolem;

        spawnManager.SetRoundAndStart(Mathf.FloorToInt(spawnTime), Mathf.FloorToInt(maxNbEnemies), Mathf.FloorToInt(nbMummy), Mathf.FloorToInt(nbMinion), Mathf.FloorToInt(nbGolem));
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////


    public void WaveEnded() {
        StartCoroutine(BetweenWavesCountdown());
        ScoreHandler.EndOfRound();
    }


    private IEnumerator BetweenWavesCountdown() {
        countDown = timeBetweenWaves;

        while (countDown > 0) {
            yield return new WaitForSeconds(1f);
            countDown--;
        }

        spawnTime += spawnTimeGrowth;
        maxNbEnemies += maxNbEnemiesGrowth;
        nbMummy += nbMummyGrowth;
        nbMinion += nbMinionGowth;
        nbGolem += nbGolemGrowth;

        spawnManager.SetRoundAndStart(Mathf.FloorToInt(spawnTime), Mathf.FloorToInt(maxNbEnemies), Mathf.FloorToInt(nbMummy), Mathf.FloorToInt(nbMinion), Mathf.FloorToInt(nbGolem));
    }


    public void PlayerDied() {
        audioSource.clip = longHurtSounds[Random.Range(0, longHurtSounds.Length)];
        audioSource.Play();

        StartCoroutine("GameEndedCountdown");
    }


    private IEnumerator GameEndedCountdown() {
        countDown = deathTimer;

        while (countDown > 0) {
            yield return new WaitForSeconds(1f);
            countDown--;
        }

        SceneManager.LoadScene(0);
    }
}
