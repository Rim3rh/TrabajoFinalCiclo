using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAsset", menuName = "Custom ScriptableObjects/Gun")]

public class GunScriptableObject : ScriptableObject
{
    [Header("Weapon information")]
    [Tooltip("max ammount of shoots before reloading")]
    public int maxMagazineAmmo;
    [Tooltip("Total ammo of this gun player can carry")]
    public int totalAmmo;
    [Tooltip("ammount of shots per second")]
    public float shootsPS;
    [Tooltip("Damage weapon deals")]
    public float damage;
    [Tooltip("Weapon Id, usualy 1+ of current weapons")]
    public int weaponID;
}
