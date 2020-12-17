using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject bars;
    public GameObject ammobars;
    public GameObject weaponsObj;
    public GameObject[] subtitles;
    public AudioSource[] subSound;
    bool introVoiceoverPlayed, moveStateCompleted;
    int currentMoveState, currentWeaponInfoState;

    enum moveStates
    {
        MoveState,
        RollState,
        ElevationState,
        SprintState
    }
    enum WeaponInfoStates
    {
        BaseInfo,
        LaserInfo,
        CannonInfo,
        TeslaInfo
    }
    
    void Start()
    {
        bars.SetActive(false);
        ammobars.SetActive(false);
        subtitles[0].SetActive(true);
        moveStateCompleted = false;
        weaponsObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (subtitles[2].activeSelf && !subSound[2].isPlaying)
        {
            introVoiceoverPlayed = true;
        }
        if (!introVoiceoverPlayed)
        {
            if (!subSound[0].isPlaying && !subSound[2].isPlaying)
            {
                subtitles[0].SetActive(false);
                subtitles[1].SetActive(true);
            }
            if (!subSound[1].isPlaying && !subSound[0].isPlaying)
            {
                subtitles[1].SetActive(false);
                subtitles[2].SetActive(true);
                
            }
        }
        if(introVoiceoverPlayed)
        {
            subtitles[2].SetActive(false);
            bars.SetActive(true);
            switch (currentMoveState)
            {
                case (int)moveStates.MoveState:
                    subtitles[3].SetActive(true);
                    if (Input.GetAxis("Forward-Back") != 0)
                    {
                        subtitles[3].SetActive(false);
                        subtitles[4].SetActive(true);
                        currentMoveState++;
                    }

                    break;
                case (int)moveStates.RollState:
                    if (Input.GetAxis("Roll") != 0)
                    {
                        subtitles[4].SetActive(false);
                        subtitles[5].SetActive(true);
                        currentMoveState++;
                    }

                    break;
                case (int)moveStates.ElevationState:
                    if (Input.GetAxis("Up-Down") != 0)
                    {
                        subtitles[5].SetActive(false);
                        subtitles[6].SetActive(true);
                        currentMoveState++;
                    }
                    break;
                case (int)moveStates.SprintState:
                    if (Input.GetAxis("Sprint") != 0)
                    {
                        subtitles[6].SetActive(false);
                        moveStateCompleted = true;
                    }
                    break;
            }
            if(moveStateCompleted)
            {
                ammobars.SetActive(true);
                weaponsObj.SetActive(true);
                switch (currentWeaponInfoState)
                {
                    case (int)WeaponInfoStates.BaseInfo:
                        subtitles[7].SetActive(true);
                        if (!subSound[3].isPlaying)
                        {
                            subtitles[7].SetActive(false);
                            subtitles[8].SetActive(true);
                            currentWeaponInfoState++;
                        }
                            break;
                    case (int)WeaponInfoStates.LaserInfo:
                        if (Input.GetAxis("Fire1") != 0 && !subSound[4].isPlaying)
                        {
                            subtitles[8].SetActive(false);
                            subtitles[9].SetActive(true);
                            currentWeaponInfoState++;
                        }

                        break;
                    case (int)WeaponInfoStates.CannonInfo:
                        if (Input.GetAxis("Fire1") != 0 && !subSound[5].isPlaying)
                        {
                            subtitles[9].SetActive(false);
                            subtitles[10].SetActive(true);
                            currentWeaponInfoState++;
                        }
                        break;
                    case (int)WeaponInfoStates.TeslaInfo:
                        if (Input.GetAxis("Fire1") != 0 && !subSound[6].isPlaying)
                        {
                            subtitles[10].SetActive(false);
                            currentWeaponInfoState++;
                        }
                        break;
                }
            }
            
        }
    }
}
