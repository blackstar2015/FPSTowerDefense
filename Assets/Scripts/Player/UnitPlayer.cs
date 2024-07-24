using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;

public class UnitPlayer : MonoBehaviour, IUnit
{
    [SerializeField] float resourcesLostOnHit = 2;
    public float unitRadius = .25f;
    AudioSource audioSource;

    bool IUnit.CanBeAttacked => true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ApplyAttack(AttackInfo aInfo)
    {
        RscManager.SpendRsc(new List<float>() { resourcesLostOnHit });
        audioSource?.Play();
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
