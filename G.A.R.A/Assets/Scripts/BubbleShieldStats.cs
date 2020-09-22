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
        //TODO: Update string variable and parameter to better type
        string weakness = "electricity";
        public void TakeDamage(int damage, string damageType)
        {
            if (damageType == weakness)
            {
                Die();
                return;
            }
            TakeDamage(damage);
        }

        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }
    }
}
