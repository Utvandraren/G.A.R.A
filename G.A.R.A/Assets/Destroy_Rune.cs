using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Rune : MonoBehaviour
{
    [SerializeField]private GameObject portalGO;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ActivatePortal()
    {
        portalGO.SetActive(true);
    }
    void Update()
    {
        
    }
}
