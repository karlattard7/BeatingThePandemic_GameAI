using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class particleHandler : MonoBehaviour
{
    public ParticleSystem part;
    public float damage = 0.5f;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit!");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        enemyBehaviour enem = other.GetComponent<enemyBehaviour>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (enem)
            {
                enem.randomBlood = false;
                enem.sound = false;
                enem.HitBloodPrefab = null;
                if(!enem.isDead()&&enem.health>0)
                    enem.TakeDamage(damage);
            }
            i++;
        }
    }
}