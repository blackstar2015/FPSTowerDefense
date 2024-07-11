using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon
{
    [SerializeField] private float _halfAngle = 60f;
    [SerializeField] private LayerMask _hitMask;

    protected override void Attack(Vector3 aimPosition, GameObject instigator)
    {
        base.Attack(aimPosition, instigator);

        Vector3 attackOrigin = instigator.transform.position;
        Vector3 attackDirection = (aimPosition - attackOrigin).normalized;

        // find all colliders within range
        Collider[] hits = Physics.OverlapSphere(attackOrigin, Range, _hitMask);

        foreach (Collider hit in hits)
        {
            // avoid self damage
            if (hit.gameObject == instigator) continue;

            // filter hits by angle
            Vector3 targetDiff = hit.transform.position - attackOrigin;
            targetDiff.y = 0f;
            Vector3 targetDir = targetDiff.normalized;
            float angleToHit = Vector3.Angle(targetDir, attackDirection);
            if (angleToHit > _halfAngle) continue;

            // damage anything with a Health component
            //if (hit.TryGetComponent(out Health health))
            //{
            //    DamageInfo damageInfo = new DamageInfo(Damage, DamageType.Fighting, hit.gameObject, instigator, instigator);
            //    health.Damage(damageInfo);
            //}
        }
    }
}
