using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using FMOD;
using Unity.VisualScripting;

public class BeatManager : MonoBehaviour
{
    public float bpm;
    public bool inBeat;



    //logic

    [SerializeField] private float bps;
    [SerializeField] private float beatTimer;


    private void Start()
    {
        beatTimer = bps;
    }
    void Update()
    {
        Beat();
    }



    private void Beat()
    {
        bps = 60/ bpm;

        beatTimer -= Time.deltaTime;

        inBeat = Mathf.Abs(beatTimer) < 0.1f;

        if (beatTimer < 0)
        {
            beatTimer = bps;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Beat");
            UnityEngine.Debug.Log(("HOLA"));
        }
    }
}
