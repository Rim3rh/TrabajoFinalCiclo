using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieWellInteractor : NetworkBehaviour
{
    [SerializeField]GameObject currentWell;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Well"))
        {
            currentWell = other.transform.parent.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Well"))
        {
            Invoke(nameof(CurrentWellToNull), 1f);
        }
    }
    private void CurrentWellToNull()
    {
        currentWell = null;

    }

    public void OnZombieDeathPs()
    {
        if (!IsServer) return;
        if (currentWell != null)
        {
            currentWell.GetComponent<WellParticleSpawner>().SpawnNewParticle(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z));

        }
    }

}
