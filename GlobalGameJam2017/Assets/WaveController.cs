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
	    particuleEffects = GetComponentInChildren(typeof(ParticuleEffects)) as ParticuleEffects;
	}

    public void chargeWave(GuitarInput[] guitarInputs)
    {
      particuleEffects.readInputs(guitarInputs);
    }

 

    
}
