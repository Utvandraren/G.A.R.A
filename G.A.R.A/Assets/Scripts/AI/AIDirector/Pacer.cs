using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : MonoBehaviour
{
    enum TempoType
    {
        BUILDUP,
        SUSTAIN,
        FADE,
        RELAX,
    }

    PlayerReader playerReader;
    float panicScore;
    float maxPanicScore = 10;
    float panicRate;
    float nodeTime;
    float levelTime;
    public bool started;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        levelTime += Time.deltaTime;
        panicScore = Mathf.Max(Mathf.Min(panicScore + panicRate * Time.deltaTime, maxPanicScore), 0);
    }

    internal void StorePlayerReader(PlayerReader playerReader)
    {
        this.playerReader = playerReader;
    }

    void CalcPanicRate()
    {
        float decrease = (maxPanicScore / (playerReader.playerStats.health / playerReader.playerStats.startingHealth)) * Time.deltaTime;
    }

    public void StartedLevel()
    {
        started = true;
    }

    public void IncreasePanic(float panicAddition)
    {
        panicScore = Mathf.Min(panicScore + panicAddition, maxPanicScore);
    }
}
