using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;

public interface IUnit
{
    bool CanBeAttacked { get; }

    Vector3 GetPos();
    float GetRadius();
    Vector3 GetTargetPoint();
    bool IsDestroyed();
    void ApplyAttack(AttackInfo aInfo);
    float GetDodge();
    bool GetImmunedToCrit();
    int GetArmorType();
    float GetDmgReduction();
}
