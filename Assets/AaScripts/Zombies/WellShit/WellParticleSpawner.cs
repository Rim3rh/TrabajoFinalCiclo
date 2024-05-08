using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WellParticleSpawner : NetworkBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    public bool isWellCompleted;
    public void SpawnNewParticle(Vector3 positionToSpawn)
    {
        //this will be run on clients
        if (isWellCompleted) return;

        SpawnClientRpc(positionToSpawn.x, positionToSpawn.y, positionToSpawn.z);
    }
    [ClientRpc]
    private void SpawnClientRpc(float posX, float posY, float posZ)
    {
        ParticleSystem ps = Instantiate(particleSystem);
        ps.transform.position = new Vector3(posX,posY,posZ);
        ps.Play();
        Destroy(ps, 2f);
    }


    [SerializeField] GameEndChecker gameEndChecker;
    [SerializeField] int maxAmmountOfSouls;
    [SerializeField] ParticleSystem endParticleSystem;
    [SerializeField] int ammountOfSouls;
    public void CheckIfWellCompleted()
    {
        //this will be run only on server
        ammountOfSouls++;
        if (ammountOfSouls >= maxAmmountOfSouls)
        {
            isWellCompleted = true;
            gameEndChecker.OneWellCompleted();
            endParticleSystem.Play();
        }
    }

}
