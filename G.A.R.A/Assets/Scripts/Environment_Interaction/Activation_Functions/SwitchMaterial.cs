using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    public Material blue, green, red;

    public bool isRed;
    public bool isBlue;
    public bool isGreen;

    // Start is called before the first frame update
    void Start()
    {
        if (isBlue)
        {
            gameObject.GetComponent<MeshRenderer>().material = blue;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlue || isGreen)
        {
            if (gameObject.TryGetComponent<ActivateInteraction>(out ActivateInteraction interaction))
            {
                if (interaction.active == ActivationEnum.On)
                {
                    if (!isBlue)
                    {
                        gameObject.GetComponent<MeshRenderer>().material = blue;
                        isBlue = true;
                        isGreen = false;
                    }
                }
                else
                {
                    if (!isGreen)
                    {
                        gameObject.GetComponent<MeshRenderer>().material = green;
                        isBlue = false;
                        isGreen = true;
                    }
                }
            }
        }
        else
        {
            if (!isRed)
            {
                gameObject.GetComponent<MeshRenderer>().material = red;
                isRed = true;
            }
        }
    }
}
