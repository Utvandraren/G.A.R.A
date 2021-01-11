using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettings : MonoBehaviour
{

    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_Text sensitivityVal;
    private PlayerController pc;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        sensitivityVal.text = (sensitivitySlider.value * 100).ToString() + "%";
    }

    public void UpdateSensitivity()
    {
        string desiredDisplayText = (sensitivitySlider.value * 100).ToString();

        //Stupid floats never ever want to cooperate
        if (sensitivitySlider.value * 100 >= 99.8f)
        {
            desiredDisplayText = "100,0";
        }
        else if(sensitivitySlider.value * 100 <= 2f)
        {
            desiredDisplayText = "1";
        }
        else
        {
            desiredDisplayText = (sensitivitySlider.value * 100).ToString().Substring(0, 4);
        }
        sensitivityVal.text = desiredDisplayText;
        sensitivityVal.text += "%";
        pc.SetSensitivity(sensitivitySlider.value);
    }
}
