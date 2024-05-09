using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class BodyLayerSetter : NetworkBehaviour
{
    #region VARS

    [SerializeField] GameObject femaleBody;
    [SerializeField] GameObject maleBody;

    //Getting the playerVisuals
     GameObject extBody;
    [SerializeField] GameObject intBody;
    #endregion
    #region SelfRunningMethods
    private void Start()
    {
        if (OwnerClientId == 0) extBody = maleBody;
        else extBody = femaleBody;


        if(IsLocalPlayer)
        {
            //if you are the local player,set intBody to true, because youy want to see it
            intBody.SetActive(true);
            //and set ext body to false so yopu cant see it
            extBody.SetActive(false);
        }
        else
        {
            //since this class will be run by every player, we set fore the not local players(the rest of the players online)
            //intbody to false
            intBody.SetActive(false);
            //Extbody to true so we can see animations etc.
            extBody.SetActive(true);
        }

    }
    public void DethCamera()
    {
        if (IsLocalPlayer)
        {

        }
        else
        {
            //since this class will be run by every player, we set fore the not local players(the rest of the players online)
            //intbody to false
            intBody.SetActive(false);
            //Extbody to true so we can see animations etc.
            extBody.SetActive(false);
        }
    }
    public void BackToNormalCamera()
    {
        if (IsLocalPlayer)
        {

        }
        else
        {
            //since this class will be run by every player, we set fore the not local players(the rest of the players online)
            //intbody to false
            intBody.SetActive(false);
            //Extbody to true so we can see animations etc.
            extBody.SetActive(true);
        }
    }

    #endregion
}
