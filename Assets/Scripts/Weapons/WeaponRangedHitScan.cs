using FMODUnity;
using GameEvents;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRangedHitScan : Weapon
{
    [SerializeField] private GameObject _waterSprayVFXGO;
    [SerializeField] private float _shotTime = 2f;
    [SerializeField] private float _rechargeRate = 5f;
    [SerializeField] private bool _isFiring = false;
    [SerializeField] private bool _isDoneFiring;
    [SerializeField] private FloatEventAsset _waterLevelPercentageAsset;
    [field: SerializeField, BoxGroup("SFX")] public EventReference ReloadWaterSFX { get; protected set; }
    private float _timeSinceStartedShooting = 0f;
    public float WaterLevelsPercentage => (1 -  _timeSinceStartedShooting / _shotTime);

    public void Fire(bool isPressed)
    {
        if (isPressed && !_isDoneFiring)
        {
            _isFiring = true;
        }
        else _isFiring = false;
        StartCoroutine(FireWeaponRoutine());
    }

    private IEnumerator FireWeaponRoutine()
    {        
        while (_isFiring && _isDoneFiring == false)
        {
            if (!FireBulletSFX.IsNull) RuntimeManager.PlayOneShot(FireBulletSFX, transform.position);
            CalculateBulletDirection();
            yield return null;
        }
        _waterSprayVFXGO.SetActive(false);
    }

    public override void CalculateBulletDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;        
        if (Physics.Raycast(ray, out hit))  targetPoint = hit.point;
        else targetPoint = ray.GetPoint(Range);

        Vector3 direction = (targetPoint - _muzzle.position).normalized;

        _waterSprayVFXGO.transform.position = _muzzle.transform.position;
        _waterSprayVFXGO.transform.rotation = Quaternion.LookRotation(direction);
        Debug.DrawRay(_muzzle.transform.position, direction, Color.red);
        _waterSprayVFXGO.SetActive(_isFiring);
        if (!Physics.Raycast(_muzzle.transform.position, direction, out RaycastHit hitInfo, Range)) return;
         if (hitInfo.collider.gameObject.TryGetComponent(out UnitCreep creep))
         {
             creep.ApplyEffect(Effect_DB.GetPrefab(5));          //5 is the slow down effect
         }
    }
    protected override void Update()
    {
        base.Update();
        _isDoneFiring = _timeSinceStartedShooting >= _shotTime;
        if (_isFiring) _timeSinceStartedShooting += Time.deltaTime;
        else if (!_isFiring || _isDoneFiring)
        {
            if (!FireBulletSFX.IsNull) RuntimeManager.PlayOneShot(FireBulletSFX, transform.position);
            _timeSinceStartedShooting -= Time.deltaTime * _rechargeRate;
        }
        _timeSinceStartedShooting = Mathf.Clamp(_timeSinceStartedShooting, 0f, _shotTime);
        _waterLevelPercentageAsset.Invoke(WaterLevelsPercentage);
    }
}
