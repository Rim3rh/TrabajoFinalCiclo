using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAsset", menuName = "Custom ScriptableObjects/Gun")]

public class GunScriptableObject : ScriptableObject
{
    [Header("WEAPONSHIT")]
    public int maxMagazineAmmo;
    public int totalAmmo;
    public float shootsPS;
    public float damage;
    public int weaponID;
}
