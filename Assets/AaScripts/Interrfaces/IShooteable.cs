using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooteable 
{
    /// <summary>
    /// Interface used so on playerInteract, we can know if a obj is shooteable or not, implements methods setplayer,
    /// so we can pass the player who is shooting, and the takedamage itself
    /// </summary>
    /// <param name="damage">Ammount of damage you want obj to recive</param>
    public void TakeDamge(float damage);
    /// <param name="player">Player shooting obj</param>
    public void SetPlayer(GameObject player);
}
