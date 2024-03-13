using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanBeShoot : MonoBehaviour
{
    public UnityEvent customEvent;


    public void ReciveShoot()
    {
        customEvent?.Invoke();
    }

}
