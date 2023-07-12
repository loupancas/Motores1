using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBullets : MonoBehaviour
{
    public ParticleSystem system;
    public int damage;
    List<ParticleCollisionEvent> collEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        system=GetComponent<ParticleSystem>();

    }

    private void OnParticleCollision(GameObject other)
    {
        int events = system.GetCollisionEvents(other,collEvents);
        print("particle hit");
        for(int I = 0 ; I < events; I++)
        {

        }

        if (other.TryGetComponent(out Player pl))
        {
            pl.TakeDamage(damage);

        }


    }


}
