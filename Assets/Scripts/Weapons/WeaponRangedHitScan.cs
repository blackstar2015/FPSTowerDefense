using System;
using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRangedHitScan : Weapon
{
    [SerializeField]private LayerMask _creepLayer;
    public bool IsCharging { get; set; } = false;
    public float CurrentCharge { get; set; } = 0f;
    [SerializeField]private bool _isFiring = false;

    public void Fire(bool isPressed)
    {
        _isFiring = isPressed;
        StartCoroutine(FireWeaponRoutine());
        //CalculateBulletDirection();
    }

    private IEnumerator FireWeaponRoutine()
    {
        while (_isFiring)
        {
            CalculateBulletDirection();
            yield return null;
        }
    }

    public override void CalculateBulletDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(Range);
        }
        Vector3 direction = (targetPoint - _muzzle.position).normalized;
        
        Debug.DrawRay(_muzzle.transform.position, direction * Range, Color.red, .5f);
        if (Physics.Raycast(_muzzle.transform.position, direction, out RaycastHit hitInfo, Range))
        {
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.collider.gameObject.TryGetComponent(out UnitCreep creep))
            {
                creep.ApplyEffect(Effect_DB.GetPrefab(5)); //5 is the slow down effect
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        //ChargingCrosshair.transform.localScale = Vector3.one;
    }
}
