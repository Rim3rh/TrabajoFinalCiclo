using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class BodyLayerSetter : NetworkBehaviour
{
    [SerializeField] GameObject extBody;
    [SerializeField] GameObject intBody;

    private void Start()
    {
        if(IsLocalPlayer)
        {
            intBody.SetActive(true);
            extBody.SetActive(false);
        }
        else
        {
            intBody.SetActive(false);
            extBody.SetActive(true);
        }

    }



}
