using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerWave : MonoBehaviour {

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
      
        IEnemy enemy = other.GetComponent<IEnemy>();



        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            enemy.TakeDamage(5);
            Debug.Log(other);
            i++;
        }
    }
}