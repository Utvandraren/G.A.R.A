using System;
public class TakeDamageEventArgs : EventArgs
{
    public int damage;
    public TakeDamageEventArgs(int damage)
    {
        this.damage = damage;
    }
}

