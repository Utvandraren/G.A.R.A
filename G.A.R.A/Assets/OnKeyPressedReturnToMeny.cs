using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyPressedReturnToMeny : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameManager.Instance.ReturnToMain();
        }
    }

  
}
