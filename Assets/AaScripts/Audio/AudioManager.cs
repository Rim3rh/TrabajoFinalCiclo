using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerWalkSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Walk");

    }
    public void PlayerJumpSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Jump");

    }
}
