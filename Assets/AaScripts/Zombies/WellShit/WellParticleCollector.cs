using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WellParticleCollector : NetworkBehaviour
{
    #region Vars
    //reference to particleSpawner
    [SerializeField] WellParticleSpawner wellParticleSpawner;
    //reference to ur own ps
    private ParticleSystem particleSystem;
    //list needed because of how ps collisions work
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    #endregion
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
            if(IsServer) wellParticleSpawner.CheckIfWellCompleted();
        }
        particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
