using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : Weapon
{
    [Header("Melee properties")]
    [SerializeField] private Transform hitposition;
    [SerializeField] private float attackDuration;
    [SerializeField] private GameObject attackBox;

    public override void Shoot()
    {
        base.Shoot();
        StartCoroutine(DealContinuousDamage());       
    }

    private IEnumerator DealContinuousDamage()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        DrawVisuals(attackBox.transform.position);
        attackBox.SetActive(false);
    }

    public override void DrawVisuals(Vector3 target)
    {
        base.DrawVisuals(target);
    }
}
