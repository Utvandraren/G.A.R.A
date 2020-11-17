using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.TryGetComponent<Animator>(out Animator a))
        {
            anim = a;
        }
    }

    public void PlayAnimation()
    {
        anim.CrossFade("Base Layer.door_2_open", 0, -1);
    }
}
