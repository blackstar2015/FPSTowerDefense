using FMODUnity;
using GameEvents;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponRangedProjectile : Weapon
{
    [field: SerializeField, BoxGroup("Weapon")] private Bullet _bulletPrefab;
    [field: SerializeField, BoxGroup("Weapon")] private float _projectileSpeed = 25f;
    [field: SerializeField, BoxGroup("Weapon")] private float _chargingFactor = 2f;    
    [field: SerializeField, BoxGroup("Weapon")] public float Cooldown { get; protected set; } = 0.5f;
    [field: SerializeField, BoxGroup("SFX")] public EventReference ChargeAttackSFX { get; protected set; }

    private float _minCharge = 0f;
    private float _maxCharge = 100f;
    public float CurrentCharge { get; set; } = 0f;
    public bool IsCharging { get; set; } = false;


    public float ChargePercentage => CurrentCharge/_maxCharge;
    
    private float _lastAttackTime;

    public void ChargeAttack()
    {
        //if(!enabled) return;
        StartCoroutine(ChargeWeaponRoutine());
    }

    public override void CalculateBulletDirection()
    {
        Rigidbody rb = SpawnProjectile();

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

        rb.velocity = direction * _projectileSpeed + direction * _projectileSpeed * ChargePercentage;
    }

    private Rigidbody SpawnProjectile()
    {
        if (!FireBulletSFX.IsNull) RuntimeManager.PlayOneShot(FireBulletSFX, transform.position);
        Bullet projectile = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
        projectile.Player = this.GetComponentInParent<PlayerControllerFPSTD>();
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        return rb;
    }

    public IEnumerator ChargeWeaponRoutine()
    {
        while(IsCharging)
        {
            if (!ChargeAttackSFX.IsNull) RuntimeManager.PlayOneShot(ChargeAttackSFX, transform.position);
            CurrentCharge += _chargingFactor * Time.deltaTime;
            CurrentCharge = Mathf.Clamp(CurrentCharge, _minCharge, _maxCharge);
            yield return null;
        }

    }

    

    protected override void Update()
    {
        base.Update();
        ChargingCrosshair.transform.localScale = Vector3.one * ChargePercentage;
    }
}