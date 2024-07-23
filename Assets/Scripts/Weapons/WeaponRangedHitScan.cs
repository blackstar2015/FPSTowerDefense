using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRangedHitScan : Weapon
{
    public void Fire()
    {
        CalculateBulletDirection();
    }
    public override void CalculateBulletDirection()
    {
        base.CalculateBulletDirection();
        RaycastHit hit;
        if(Physics.Raycast(ChargingCrosshair.transform.position,Camera.main.transform.forward, out hit,Range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(ChargingCrosshair.transform.forward, hit.point);
            if(hit.collider.gameObject.TryGetComponent(out UnitCreep creep))
            {
                creep.ApplyEffect(Effect_DB.GetPrefab(5)); //5 is the slow down effect
            }
        }
    }
}
