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
    public bool coordinationTrigger;

    public bool isBeatPlaying;


    //logic

    private float bps;
    [SerializeField] private float beatTimer;

    [SerializeField] GameObject beatIndicator;


    float cancelCombo;
    private void Start()
    {
        beatTimer = bps;
    }
    void Update()
    {
        if(isBeatPlaying) Beat();
        if (inBeat) beatIndicator.SetActive(true); else beatIndicator.SetActive(false);


        cancelCombo -= Time.deltaTime;
        if (cancelCombo < 0) CancelCombo();
    }

    
    public void BeatToggle()
    {
        UnityEngine.Debug.Log("hola");

        if (isBeatPlaying)
        {
            isBeatPlaying = false;
        }
        else
        {
            isBeatPlaying = true;
        }
    }

    private void Beat()
    {
        bps = 60/ bpm;

        beatTimer -= Time.deltaTime;

        if (Mathf.Abs(beatTimer) < 0.15f || beatTimer > bps - 0.15f)
        {
            inBeat = true;
        }
        else
        {
            inBeat = false;
        }

        coordinationTrigger = false;
        if (beatTimer < 0)
        {
            beatTimer = bps;
            AudioManager.instance.Beat();
            coordinationTrigger = true;
        }
    }


    private int comboAmmount;
    private int comboMultiplyer;
    [SerializeField] int pointsGained;
    [SerializeField] GameObject player;
    [SerializeField] UiManager uiManager;
    public void AddToCombo()
    {
        comboMultiplyer++;
        comboAmmount += comboMultiplyer * pointsGained;
        uiManager.UpdateCombos(comboAmmount, comboMultiplyer);
        cancelCombo = 2f;
    }

    public void CancelCombo()
    {
        player.GetComponent<PlayerManager>().PlayerPoints += comboAmmount;
        comboMultiplyer = 0;
        comboAmmount = 0;
        uiManager.CleanCombos();

    }
}
