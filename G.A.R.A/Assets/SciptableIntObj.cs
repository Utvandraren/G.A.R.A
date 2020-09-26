using UnityEngine;

[CreateAssetMenu(fileName = "newIntData", menuName = "SciptableObjects/New intData", order = 0)]
public class SciptableIntObj : ScriptableObject
{
    public int startValue;

    [HideInInspector] public int value;

    public void ResetValue()
    {
        value = startValue;
    }

}
