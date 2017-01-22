using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip themeMusic;
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
        audioSource.loop = true;
        audioSource.clip = themeMusic;
        audioSource.Play();

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
}
