using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public GameObject mummy;
    public GameObject minion;
    public GameObject golem;

    private List<GameObject> ennemyAvailable;
    private Transform[] spawnPoints;
    private Transform player;
    private int nbEnnemy = 0;
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
        ennemyAvailable = new List<GameObject>();
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
        nbEnnemy = 0;

        ennemyAvailable.Clear();
        ennemyAvailable.Add(mummy);
        ennemyAvailable.Add(minion);
        ennemyAvailable.Add(golem);

        CancelInvoke("SpawnEnnemy");
        InvokeRepeating("SpawnEnnemy", spawnTime, spawnTime);
    }


    private void SpawnEnnemy() {
        if(nbEnnemy >= maxEnnemyOnScene || ennemyAvailable.Count == 0) {
            return;
        }

        nbEnnemy++;
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        Instantiate(nextEnnemy(), spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }


    private void SortSpawnsByDistance() {
        Vector3 playerPosition = player.position;

        for(int i = 1; i < spawnPoints.Length; i++) {
            if(Vector3.Distance(player.position, spawnPoints[i].position) < Vector3.Distance(player.position, spawnPoints[i - 1].position)) {
                Transform temp = spawnPoints[i];
                spawnPoints[i] = spawnPoints[i - 1];
                spawnPoints[i - 1] = temp;
            }
        }
    }


    private GameObject nextEnnemy() {
        int ennemyIndex = Random.Range(0, ennemyAvailable.Count);
        GameObject newEnnemy = ennemyAvailable[ennemyIndex];

        if( newEnnemy == mummy) {
            nbMummyLeft--;
            if(nbMummyLeft == 0) {
                ennemyAvailable.Remove(mummy);
            }
        }
        else if (newEnnemy == minion) {
            nbMinionLeft--;
            if (nbMinionLeft == 0) {
                ennemyAvailable.Remove(minion);
            }
        }
        else if (newEnnemy == golem) {
            nbGolemLeft--;
            if (nbGolemLeft == 0) {
                ennemyAvailable.Remove(golem);
            }
        }

        return newEnnemy;
    }


    public void EnnemyDied() {
        nbEnnemy--;
    }
}
