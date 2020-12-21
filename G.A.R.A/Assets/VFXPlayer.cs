using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


/// <summary>
/// This class is used to play visual effects that aren't supposed to loop but you want to trigger in an animation.
/// Mostly used for muzzle flashes and similar effects created using visual effect graphs.
/// If there is a better way to do it, I don't know it.
/// </summary>
public class VFXPlayer : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;

    public void TriggerEffect()
    {
        vfx.Play();
    }
}
