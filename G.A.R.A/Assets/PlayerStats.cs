using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public override void Die()
    {
        base.Die();
        Debug.Log("Player died");
        //Gamemanager.tooglegameoverscreen/loosegame();   <-- To be added later
    }
}
