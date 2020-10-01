using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Stats for bubble Shields
    /// </summary>
    class ShielderStats : EnemyStats
    {
        public int shieldHealth = 50;
        public GameObject shield;
        public override void TakeDamage(SciptableAttackObj attack)
        {
            if (attack.element == SciptableAttackObj.WeaponElement.Electricity)
            {
                shieldHealth = 0;
                return;
            }
            if (shieldHealth <= 0)
                base.TakeDamage(attack);
            else
                DamageShield(attack);
        }
        private void DamageShield(SciptableAttackObj attack)
        {
            shieldHealth -= attack.damage;
            shieldHealth = Mathf.Max(0, shieldHealth);
            if (shieldHealth <= 0)
                shield.SetActive(false);
        }
    }
}
