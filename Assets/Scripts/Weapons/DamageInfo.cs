using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public float Amount { get; set; }
    public DamageType DamageType { get; set; }
    public GameObject Victim { get; set; }      // actor getting damaged
    public GameObject Instigator { get; set; }  // actor doing the damage
    public GameObject Source { get; set; }      // *thing* doing the damage (bullet/grenade/rocket)

    public DamageInfo(float amount, DamageType damageType, GameObject victim, GameObject instigator, GameObject source)
    {
        Amount = amount;
        DamageType = damageType;
        Victim = victim;
        Instigator = instigator;
        Source = source;
    }
}

public enum DamageType
{
    None,
    Physical,
    Emotional,
    Fire,
    Water,
    Earth,
    Air,
    Dragon,
    Fairy,
    Fighting
}