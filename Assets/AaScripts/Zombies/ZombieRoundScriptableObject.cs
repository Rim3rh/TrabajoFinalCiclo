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
        [Tooltip("Game Round")]
        public int round;
        [Tooltip("Zombies to spawn in that round")]
        public int ammountOfZombies;
        [Tooltip("heealth zombies of this round will have")]
        public int zombiesHealth;
        [Tooltip("speed zombies of this round will have")]
        public float zombiesSpeed;
        [Tooltip("rate at wich zombies will spawn")]
        public float spawnRate;
        [Tooltip("how many zombies can there be at the same time")]
        public int zombiePoolSize;
    }
    [SerializeField]
    public List<Rounds> rounds = new List<Rounds>();    
}
