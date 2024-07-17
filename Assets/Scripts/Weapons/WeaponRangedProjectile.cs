using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponRangedProjectile : Weapon
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _projectileSpeed = 25f;
    [SerializeField] private float _chargingFactor = 2f;

    private GameObject ChargingCrosshair;
    private float _minCharge = 0f;
    private float _maxCharge = 100f;
    public float CurrentCharge { get; set; } = 0f;
    public float ChargePercentage => CurrentCharge/_maxCharge;
    public bool IsCharging { get; set; } = false;

    protected override void Attack(Vector3 aimPosition,  GameObject instigator)
    {
        base.Attack(aimPosition, instigator);
        
        CalculateBulletDirection();
    }

    public void ChargeAttack()
    {
        StartCoroutine(ChargeWeaponRoutine());
    }

    public void CalculateBulletDirection()
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

        rb.velocity = direction * _projectileSpeed * ChargePercentage;
    }

    private Rigidbody SpawnProjectile()
    {
        Bullet projectile = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
        projectile.Player = this.GetComponentInParent<PlayerControllerFPSTD>();
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        return rb;
    }

    public IEnumerator ChargeWeaponRoutine()
    {
        while(IsCharging)
        {
            CurrentCharge += _chargingFactor * Time.deltaTime;
            CurrentCharge = Mathf.Clamp(CurrentCharge, _minCharge, _maxCharge);
            yield return null;
        }

    }

    public void SetChargeCrosshair(GameObject crosshair)
    {
        ChargingCrosshair = crosshair;
    }

    protected override void Update()
    {
        base.Update();
        ChargingCrosshair.transform.localScale = Vector3.one * ChargePercentage;
    }
}