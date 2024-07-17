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
    [field: SerializeField, InlineButton(nameof(FindWeapons), Label = "Find")] protected WeaponRangedProjectile[] Weapons { get; private set; }
    private bool IsActive = true;

    public  void OnChargeAttack(InputValue value)
    {
        // assume weapon 0 is shotgun
        if (!Weapons[0].isActiveAndEnabled) return;
        WeaponRangedProjectile currentWeapon = Weapons[0];
        currentWeapon.IsCharging = true;
        currentWeapon.ChargeAttack();
        //currentWeapon.TryAttack(_aimPosition, gameObject);
    }

    public void OnReleaseAttack(InputValue value)
    {
        if (!Weapons[0].isActiveAndEnabled) return;
        WeaponRangedProjectile currentWeapon = Weapons[0];
        currentWeapon.IsCharging = false;
        currentWeapon.CalculateBulletDirection();
        currentWeapon.CurrentCharge = 0f;
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
