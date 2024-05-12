using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Make it SIngleTone so can acces from all places
    /// </summary>
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    #region PlayerSounds
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
    #endregion
    #region WeaponSounds
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
    public void ReloadSfx()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Guns/GenericReload");
    }
    #endregion
    #region MapSounds
    public void BuyFromShop()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shop/Buy");
    }
    #endregion
    #region Music
    #endregion










}
