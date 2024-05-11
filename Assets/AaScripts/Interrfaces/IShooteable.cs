using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooteable 
{
    public void TakeDamge(float damage);
    public void SetPlayer(GameObject player);
}
