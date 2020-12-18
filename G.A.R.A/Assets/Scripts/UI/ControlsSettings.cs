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
        if (desiredDisplayText.Substring(0, 4) == "99,8")
        {
            desiredDisplayText = "100,0";
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
