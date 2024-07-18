using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;

public class UnitPlayer : MonoBehaviour, IUnit
{
    // TODO: Instead of inheriting from Unit, make an IUnit interface with GetPos and GetRadius methods and have this and Unit implement that. Then you can get rid of all the other Unit junk in here.

    [SerializeField] float resourcesLostOnHit = 2;
    public float unitRadius = .25f;

    bool IUnit.CanBeAttacked => true;

    public void ApplyAttack(AttackInfo aInfo)
    {
        RscManager.SpendRsc(new List<float>() { resourcesLostOnHit });
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public float GetRadius()
    {
        return unitRadius;
    }

    Vector3 IUnit.GetTargetPoint()
    {
        return transform.position;
    }

    bool IUnit.IsDestroyed()
    {
        return false;
    }

    float IUnit.GetDodge()
    {
        return 0;
    }

    bool IUnit.GetImmunedToCrit()
    {
        return false;
    }

    int IUnit.GetArmorType()
    {
        return 0;
    }

    float IUnit.GetDmgReduction()
    {
        return 0;
    }
}
