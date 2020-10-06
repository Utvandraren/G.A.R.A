using UnityEngine;

[CreateAssetMenu(fileName = "newIntData", menuName = "SciptableObjects/New intData", order = 0)]
public class SciptableIntObj : ScriptableObject
{
    public int startValue;

    public int value;

    public void ResetValue()
    {
        value = startValue;
    }

}
