using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    //External perceptions
    public Transform PlayerTransform;
    //Internal perceptions
    public float detectionRange;
    public Stats stats;
    internal bool readyForApproach;

    //Dependent perceptions
    internal Vector3 target;
    public float weaponMaxRange = 3;
    public float weaponMinRange = 1;

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}