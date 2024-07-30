using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMovement;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using Cinemachine;
using GameEvents;

public class PlayerControllerFPSTD : PlayerController
{
    private Vector3 _aimPosition;

    [field: Header("Weapons")]
    [field: SerializeField] protected WeaponRangedProjectile weaponRangedProjectile{ get; private set; }
    [field: SerializeField] protected WeaponRangedHitScan weaponRangedHitScan{ get; private set; }

    [SerializeField, FoldoutGroup("Events")] private BoolEventAsset _hitScanEnabledEvent;
    [SerializeField, FoldoutGroup("Events")] private BoolEventAsset _ProjectileEnabledEvent;

    private bool IsActive;
    private bool _isFiring;
    private GameObject _currentWeapon;
    private float _nextAttackTime;
    private float _coolDown => weaponRangedProjectile.Cooldown;

    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorMode;
        _currentWeapon = weaponRangedProjectile.gameObject;
        IsActive = _currentWeapon.gameObject.activeSelf;
    }

    public  void OnChargeAttack(InputValue value)
    {
        if (!weaponRangedProjectile.isActiveAndEnabled || Time.time < _nextAttackTime) return;
        weaponRangedProjectile.IsCharging = true;
        weaponRangedProjectile.ChargeAttack();
    }

    public void OnReleaseAttack(InputValue value)
    {
        if (!weaponRangedProjectile.isActiveAndEnabled || Time.time < _nextAttackTime) return;
        weaponRangedProjectile.IsCharging = false;
        weaponRangedProjectile.CalculateBulletDirection();
        weaponRangedProjectile.CurrentCharge = 0f;
        _nextAttackTime = Time.time + _coolDown;
    }
    public void OnShoot(InputValue value)
    {
        if (!weaponRangedHitScan.isActiveAndEnabled) return;
        _isFiring = value.isPressed;
        weaponRangedHitScan.Fire(_isFiring);
    }

    public void OnSwitchWeapons()
    {
        if (_currentWeapon == weaponRangedProjectile.gameObject)
        {
            weaponRangedProjectile.gameObject.SetActive(false);
            weaponRangedHitScan.gameObject.SetActive(true);
            _currentWeapon = weaponRangedHitScan.gameObject;
        }
        else
        {
            weaponRangedHitScan.gameObject.SetActive(false);
            weaponRangedProjectile.gameObject.SetActive(true);
            _currentWeapon = weaponRangedProjectile.gameObject;
        }
    }

    public void OnToggleWeapon(InputValue value)
    {
        IsActive = !IsActive;
        _currentWeapon.gameObject.SetActive(IsActive);
    }
    protected override void Update()
    {
        base.Update();
        _hitScanEnabledEvent.Invoke(weaponRangedHitScan.gameObject.activeSelf);
        _ProjectileEnabledEvent.Invoke(weaponRangedProjectile.gameObject.activeSelf);
    }
}
