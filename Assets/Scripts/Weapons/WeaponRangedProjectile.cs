using Cinemachine;
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

    [SerializeField, BoxGroup("Camera")] private float _minFOV = 30f;
    [SerializeField, BoxGroup("Camera")] private float _maxFOV = 60f;
    [SerializeField, BoxGroup("Camera")] private CinemachineVirtualCamera _camera;

    private float _minCharge = 0f;
    private float _maxCharge = 100f;
    public float CurrentCharge { get; set; } = 0f;
    public bool IsCharging { get; set; } = false;
    public float ChargePercentage => CurrentCharge/_maxCharge;

    public void ChargeAttack()
    {
        StartCoroutine(ChargeWeaponRoutine());
    }

    public override void CalculateBulletDirection()
    {
        Rigidbody rb = SpawnProjectile();

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))    targetPoint = hit.point;
        else targetPoint = ray.GetPoint(Range);

        Vector3 direction = (targetPoint - _muzzle.position).normalized;
        rb.linearVelocity = direction * _projectileSpeed + direction * _projectileSpeed * ChargePercentage;
    }

    private Rigidbody SpawnProjectile()
    {
        if (!FireBulletSFX.IsNull) RuntimeManager.PlayOneShot(FireBulletSFX, transform.position);
        Bullet projectile = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
        projectile.Player = this.GetComponentInParent<PlayerControllerFPSTD>();
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        _camera.m_Lens.FieldOfView = _maxFOV;
        return rb;
    }

    public IEnumerator ChargeWeaponRoutine()
    {
        while(IsCharging)
        {
            if (!ChargeAttackSFX.IsNull) RuntimeManager.PlayOneShot(ChargeAttackSFX, transform.position);
            CurrentCharge += _chargingFactor * Time.deltaTime;
            CurrentCharge = Mathf.Clamp(CurrentCharge, _minCharge, _maxCharge);
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_maxFOV, _minFOV, CurrentCharge / 100);
            yield return null;
        }
    }

    protected override void Update()
    {
        base.Update();
        ChargingCrosshair.transform.localScale = Vector3.one * ChargePercentage;
    }
}