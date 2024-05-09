using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkAnimator : NetworkAnimator
{
    //will make the networkanimator work on clinets
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
