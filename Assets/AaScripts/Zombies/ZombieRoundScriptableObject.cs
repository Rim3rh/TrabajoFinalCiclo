using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAsset", menuName = "Custom ScriptableObjects/Rounds")]
public class ZombieRoundScriptableObject : ScriptableObject
{
    [Serializable]
    public class Rounds
    {
        public int round;
        public int ammountOfZombies;
        public int zombiesHealth;
        public int zombiesSpeed;
        public float spawnRate;
    }
    [SerializeField]
    public List<Rounds> rounds = new List<Rounds>();    
}
