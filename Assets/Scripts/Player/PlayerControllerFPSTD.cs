using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMovement;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class PlayerControllerFPSTD : PlayerController
{
    private Vector3 _aimPosition;

    [field: Header("Weapons")]
    [field: SerializeField, InlineButton(nameof(FindWeapons), Label = "Find")] protected Weapon[] Weapons { get; private set; }
    private bool IsActive;

    protected override void Awake()
    {
        base.Awake();
        IsActive = Weapons[0].gameObject.activeSelf;
    }

    public  void OnChargeAttack(InputValue value)
    {
        if (!Weapons[0].isActiveAndEnabled) return;
        if(Weapons[0].TryGetComponent(out WeaponRangedProjectile projectileWeapon))
        {
            projectileWeapon.IsCharging = true;
            projectileWeapon.ChargeAttack();
        }
    }

    public void OnReleaseAttack(InputValue value)
    {
        if (!Weapons[0].isActiveAndEnabled) return;
        if (Weapons[0].TryGetComponent(out WeaponRangedProjectile projectileWeapon))
        {            
            projectileWeapon.IsCharging = false;
            projectileWeapon.CalculateBulletDirection();
            projectileWeapon.CurrentCharge = 0f;

        }
    }
    public void OnShoot(InputValue value)
    {
        if (!Weapons[0].isActiveAndEnabled) return;
        if (Weapons[0].TryGetComponent(out WeaponRangedHitScan hitScanWeapon))
        {
            hitScanWeapon.Fire();
        }
    }
    // call from inspector button
    private void FindWeapons()
    {
        Weapons = GetComponentsInChildren<WeaponRangedProjectile>();
    }

    public void OnToggleWeapon(InputValue value)
    {
        IsActive = !IsActive;
        Weapons[0].gameObject.SetActive(IsActive);
    }
}
