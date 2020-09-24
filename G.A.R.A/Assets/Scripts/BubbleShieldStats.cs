using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    /// <summary>
    /// Stats for bubble Shields
    /// </summary>
    class BubbleShieldStats : Stats
    {
        public override void TakeDamage(SciptableAttackObj attack)
        {
            if (attack.element == SciptableAttackObj.WeaponElement.Electricity)
            {
                Die();
                return;
            }
            base.TakeDamage(attack);
        }

        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }
    }
}
