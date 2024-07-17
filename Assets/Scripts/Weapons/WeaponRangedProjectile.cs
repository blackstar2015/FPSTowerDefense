using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedProjectile : Weapon
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _projectileSpeed = 25f;
    [SerializeField] private int _shotCount = 6;
    [SerializeField] private float _inaccuracy = 10f;

    protected override void Attack(Vector3 aimPosition,  GameObject instigator)
    {
        base.Attack(aimPosition, instigator);

        //Vector3 spawnPos = _muzzle.position;
        //Vector3 aimDir = _muzzle.transform.forward;
        //Quaternion spawnRot = Quaternion.LookRotation(aimDir);
        Bullet projectile = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

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

        // Apply force to the projectile
        rb.velocity = direction * _projectileSpeed;


    }
}