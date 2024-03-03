using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        this.transform.position = player.transform.position;
    }
}
