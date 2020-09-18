using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    //External perceptions
    public Transform PlayerTransform;
    //Internal perceptions
    public float detectionRange;

    //Dependent perceptions
    internal Vector3 target;
    internal float weaponRange;
}