using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class AirZone : MonoBehaviour
{
    ParticleSystem ps;

    private void OnCollisionEnter()
    {
        Debug.Log("sada");
    }

    //private void OnParticleCollision(ParticleSystem particle)
    //{

    //    ps = particle;


    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    //}
}
