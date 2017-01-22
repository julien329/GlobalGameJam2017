using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModelData : MonoBehaviour {

    [SerializeField]
    Text timer;
    [SerializeField]
    Text score;
    [SerializeField]
    GameObject health;
    [SerializeField]
    Text waveNumber;
    [SerializeField]
    Text remainingTargets;

    float timeSpent;
    int currScore;
    int currWave;
    int currEnnemies;
    
	// Use this for initialization
	void Start ()
    {
        timeSpent = 0.0f;
        currEnnemies = 0;
        currWave = 1;
        currScore = 0;	
        //score.text = "Fame: 0";
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Increment time
        timeSpent += Time.deltaTime;
	}

    //Call in cas of a new wave in order to increment the wave number and curr ennemies
    public void InitiateNewWave(int nMonsters)
    {
        currWave++;
        currEnnemies = nMonsters;

        //waveNumber.text = "Wave Number: " + currWave;
        //currEnnemies.text = "Ennemies remaining: " + currEnnemies;
    }

    public void EnnemyDown()
    {
        currEnnemies--;
        //currEnnemies.text = "Ennemies remaining: " + currEnnemies;
    }

    public void IncrementScore(int ammount)
    {
        currScore += ammount;
        //score.text = "Fame: " + currScore;
    }
}
