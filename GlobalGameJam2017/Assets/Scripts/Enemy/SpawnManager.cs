using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public GameObject mummy;
    public GameObject minion;
    public GameObject golem;

    private List<GameObject> enemyAvailable;
    private GameFlow gameFlow;
    private Transform[] spawnPoints;
    private Transform player;
    private Text roundNumberText;
    private Text remainingTargets;
    private int waveNumber = 0;
    private int remainingEnemies;
    private int nbEnemyOnScene = 0;
    private int spawnTime;
    private int maxEnnemyOnScene;
    private int nbMummyLeft;
    private int nbMinionLeft;
    private int nbGolemLeft;
    private const int NB_SPAWNS = 6;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake () {
        roundNumberText = GameObject.Find("WaveCounterValue").GetComponent<Text>();
        remainingTargets = GameObject.Find("EnemyCounterValue").GetComponent<Text>();
        gameFlow = GetComponent<GameFlow>();
        enemyAvailable = new List<GameObject>();
        player = GameObject.Find("Player").transform;
        spawnPoints = new Transform[NB_SPAWNS];
        for (int i = 0; i < NB_SPAWNS; i++) {
            spawnPoints[i] = GameObject.Find("Spawn " + (i + 1)).transform;
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void SetRoundAndStart(int spawnTime, int maxEnnemyOnScene, int nbMummyLeft, int nbMinionLeft, int nbGolemLeft) {
        this.spawnTime = spawnTime;
        this.maxEnnemyOnScene = maxEnnemyOnScene;
        this.nbMummyLeft = nbMummyLeft;
        this.nbMinionLeft = nbMinionLeft;
        this.nbGolemLeft = nbGolemLeft;
        roundNumberText.text = (++waveNumber).ToString();
        remainingEnemies = nbMummyLeft + nbMinionLeft + nbGolemLeft;
        remainingTargets.text = remainingEnemies.ToString();
        nbEnemyOnScene = 0;

        enemyAvailable.Clear();
        if (nbMummyLeft > 0) {
            enemyAvailable.Add(mummy);
        }
        if (nbMinionLeft > 0) {
            enemyAvailable.Add(minion);
        }
        if (nbGolemLeft > 0) {
            enemyAvailable.Add(golem);
        }

        CancelInvoke("SpawnEnnemy");
        InvokeRepeating("SpawnEnnemy", spawnTime, spawnTime);
    }


    private void SpawnEnnemy() {
        if(nbEnemyOnScene >= maxEnnemyOnScene || enemyAvailable.Count == 0 || !player) {
            return;
        }

        nbEnemyOnScene++;
        SortSpawnsByDistance();
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        Instantiate(nextEnnemy(), spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }


    private void SortSpawnsByDistance() {
        for (int i = 0; i < spawnPoints.Length; i++) {
            for (int j = i + 1; j < spawnPoints.Length; j++) {
                if (Vector3.Distance(player.position, spawnPoints[i].position) > Vector3.Distance(player.position, spawnPoints[j].position)) {
                    Transform temp = spawnPoints[i];
                    spawnPoints[i] = spawnPoints[j];
                    spawnPoints[j] = temp;
                }
            }
        }
    }


    private GameObject nextEnnemy() {
        int ennemyIndex = Random.Range(0, enemyAvailable.Count);
        GameObject newEnnemy = enemyAvailable[ennemyIndex];

        if( newEnnemy == mummy) {
            nbMummyLeft--;
            if(nbMummyLeft == 0) {
                enemyAvailable.Remove(mummy);
            }
        }
        else if (newEnnemy == minion) {
            nbMinionLeft--;
            if (nbMinionLeft == 0) {
                enemyAvailable.Remove(minion);
            }
        }
        else if (newEnnemy == golem) {
            nbGolemLeft--;
            if (nbGolemLeft == 0) {
                enemyAvailable.Remove(golem);
            }
        }

        return newEnnemy;
    }


    public void EnnemyDied() {
        nbEnemyOnScene--;
        remainingEnemies--;
        remainingTargets.text = remainingEnemies.ToString();
        if (nbEnemyOnScene == 0 && enemyAvailable.Count == 0) {
            CancelInvoke("SpawnEnnemy");
            gameFlow.WaveEnded();
        }
    }
}
