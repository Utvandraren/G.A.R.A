using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : MonoBehaviour
{
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
        if (started)
            levelTime += Time.deltaTime;
        panicScore = Mathf.Min(panicScore += panicRate * Time.deltaTime, maxPanicScore);
    }

    internal void GivePlayerReader(PlayerReader playerReader)
    {
        this.playerReader = playerReader;
    }

    void CalcPanicRate()
    {
        float decrease = (1 / playerReader.GetPlayerHP()) * Time.deltaTime;
    }

    public void StartedLevel(int playerNode)
    {
        started = true;
    }
}
