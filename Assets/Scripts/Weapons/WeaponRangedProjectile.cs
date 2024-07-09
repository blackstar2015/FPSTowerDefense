using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedProjectile : Weapon
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private float _projectileSpeed = 25f;
    [SerializeField] private int _shotCount = 6;
    [SerializeField] private float _inaccuracy = 10f;

    protected override void Attack(Vector3 aimPosition,  GameObject instigator)
    {
        base.Attack(aimPosition, instigator);

        Vector3 spawnPos = _muzzle.position;
        // direction from A to B is B minus A, normalized
        Vector3 aimDir = _muzzle.transform.forward;
        // find spawn rotation from aim direction
        Quaternion spawnRot = Quaternion.LookRotation(aimDir);

        for (int i = 0; i < _shotCount; i++)
        {
            // randomly generate inaccuracy
            float inaccX = Random.Range(-_inaccuracy, _inaccuracy);
            float inaccY = Random.Range(-_inaccuracy, _inaccuracy);

            // create a rotation from inaccuracy
            Quaternion inaccRot = Quaternion.Euler(_muzzle.up * inaccX + _muzzle.right * inaccY);

            // combine spawn and inaccuracy rotations - we combine quaternions by multiplying them
            Quaternion finalRotation = spawnRot * inaccRot;

            // spawn projectile and assign initial values
            Projectile spawnedProjectile = Instantiate(_bulletPrefab, spawnPos, finalRotation);
            spawnedProjectile.Damage = Damage;
            spawnedProjectile.DamageType = DamageType;
            spawnedProjectile.Speed = _projectileSpeed;
            spawnedProjectile.Range = Range;
            spawnedProjectile.Instigator = instigator;
        }
    }
}