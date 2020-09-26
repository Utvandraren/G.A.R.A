using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "SciptableObjects/New attackData", order = 1)]
public class SciptableAttackObj : ScriptableObject
{
    public int damage;

    public enum WeaponElement
    {
        Laser,     
        Explosive,
        Electricity
    }

    public WeaponElement element;
  
}
