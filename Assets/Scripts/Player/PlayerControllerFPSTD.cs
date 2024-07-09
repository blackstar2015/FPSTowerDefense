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

    public override void OnFire(InputValue value)
    {
        // assume weapon 0 is shotgun
        Weapon currentWeapon = Weapons[0];
        currentWeapon.TryAttack(_aimPosition, gameObject);
    }

    // call from inspector button
    private void FindWeapons()
    {
        Weapons = GetComponentsInChildren<Weapon>();
    }
}
