using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ZombieWellInteractor : NetworkBehaviour
{
    //gameobjkect well trhat will be asigned 
    GameObject currentWell;
    #region SelfRunningMethods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Well"))
        {
            //assing well
            currentWell = other.transform.parent.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Well"))
        {
            //set it to null with delay
            StartCoroutine(CurrentWellToNull());
        }
    }
    #endregion
    #region public methods
    public IEnumerator CurrentWellToNull()
    {
        yield return new WaitForSeconds(1f);
        currentWell = null;
    }
    public void OnZombieDeathPs()
    {
        //only done on server
        if (!IsServer) return;
        //spawn particle if well is not = to null
        if (currentWell != null)
        {
            currentWell.GetComponent<WellParticleSpawner>().SpawnNewParticle(
            new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z));
        }
    }
    #endregion
}
