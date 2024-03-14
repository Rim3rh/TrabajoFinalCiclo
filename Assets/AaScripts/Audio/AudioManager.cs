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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/SwitchWeapon");

    }


    public void PistolSfxShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/Pistol/PistolShot");

    }
    public void AkSfxShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/Ak/AkShot");

    }

    public void NoAmmoShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/NoAmmo");

    }



    public void BuyFromShop()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shop/Buy");

    }

    public void FenceOpen()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shop/Puerta_Metal");

    }
    public void EnemyHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dummy/DummyHit");

    }
    public void EnemyMove()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dummy/DummyMovement");

    }

    public void GameStart()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shop/Puerta_Metal");

    }




    public void ReloadSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/GenericReload");

    }
    public void Beat()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Beat/Beat");

    }





}
