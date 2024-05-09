using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WellParticleCollector : NetworkBehaviour
{
    private ParticleSystem particleSystem;
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();


    [SerializeField] WellParticleSpawner wellParticleSpawner;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnParticleTrigger()
    {
        int triggeredParticles = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.remainingLifetime = 0;
            particles[i] = particle;
            //we only want the well logic happening on the server
            Debug.Log(this.gameObject);

            wellParticleSpawner.CheckIfWellCompleted();
            


        }
        particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }



}
