using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public Transform[] spawnPoints;
    public Transform player;
    public GameObject mummy;
    public GameObject minion;
    public GameObject golem;
    public int spawnTime;
    public int maxEnnemyOnScene;
    public int nbMummyLeft;
    public int nbMinionLeft;
    public int nbGolemLeft;

    private int nbEnnemy = 0;
    private List<GameObject> ennemyAvailable;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Start () {
        ennemyAvailable = new List<GameObject>();
        ennemyAvailable.Add(mummy);
        ennemyAvailable.Add(minion);
        ennemyAvailable.Add(golem);

        InvokeRepeating("SpawnEnnemy", spawnTime, spawnTime);
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

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
