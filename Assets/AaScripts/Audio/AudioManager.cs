using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

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
    public void PlayerWalkSfx(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Player/Walk", position);
    }
    public void PlayerJumpSfx(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Player/Jump", position);
    }
    public void PlayerLandSfx(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Player/Land", position);
    }
    public void PlayerSwitchWeapon(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Guns/SwitchWeapon", position);
    }
    #endregion
    #region WeaponSounds
    public void PlayGunShoot(int weaponId, Vector3 position)
    {
        switch (weaponId)
        {

            case 1:

                PlayOneShotServerRpc("event:/SFX/Guns/Pistol/PistolShot", position);

                break;

            case 2:
                PlayOneShotServerRpc("event:/SFX/Guns/Ak/AkShot", position);

                break;

            case 3:

                PlayOneShotServerRpc("event:/SFX/Guns/Ak/AkShot", position);

                break;

            case 4:
                PlayOneShotServerRpc("event:/SFX/Guns/Pistol/PistolShot", position);

                break;
        }


    }

    public void NoAmmoShoot(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Guns/NoAmmo", position);
    }
    public void ReloadSfx(Vector3 position)
    {
        PlayOneShotServerRpc("event:/SFX/Guns/GenericReload", position);
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

    #region Network
    [ServerRpc(RequireOwnership =false)]
    private void PlayOneShotServerRpc(string path, Vector3 position)
    {
        PlayOneShotClientRpc(path, position);
    }
    [ClientRpc]

    private void PlayOneShotClientRpc(string path, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, position);
    }

    #endregion








}
