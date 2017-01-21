using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private ParticuleEffects particuleEffects;
    public GuitarInput[] GuitarInputs;
	// Use this for initialization
	void Start ()
	{
	    particuleEffects = GetComponent<ParticuleEffects>();
	}

    void chargeWave(GuitarInput[] guitarInputs)
    {
      particuleEffects.readInputs(guitarInputs);
    }

    void Update()
    {
        if (Input.anyKey && GuitarInputs.Length>0)
        {
            chargeWave(GuitarInputs);
        }   
    }


    
}
