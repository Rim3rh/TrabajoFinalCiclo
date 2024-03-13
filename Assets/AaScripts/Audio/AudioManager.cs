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
    public void PlayerLandSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Land");

    }
    public void PlayerSwitchWeapon()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/GenericReload");

    }


    public void PistolSfxShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/Pistol/PistolShot");

    }
    public void AkSfxShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/Ak/AkShot");

    }
    public void ReloadSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/GenericReload");

    }






}
