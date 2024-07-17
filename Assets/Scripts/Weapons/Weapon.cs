using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// FMOD namespaces:
using FMOD.Studio;
using FMODUnity;
using TDTK;
using GameEvents;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Weapon")] public float Damage { get; protected set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public float Range { get; protected set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public float Cooldown { get; protected set; } = 0.5f;
    [field: SerializeField, BoxGroup("Weapon")] public BoolEventAsset WeaponEnabledEvent { get; protected set; }

    [field: SerializeField, BoxGroup("Animation")] public Animator Animator { get; protected set; }
    [field: SerializeField, BoxGroup("Animation")] public string AnimationTrigger { get; protected set; }

    [field: SerializeField, BoxGroup("SFX")] public EventReference AttackSFX { get; protected set; }

    // TODO: sound FX
    // TODO: VFX
    // TODO: animation

    private float _lastAttackTime;

    private void OnValidate()
    {
        if (Animator == null) Animator = GetComponentInParent<Animator>();
    }

        // handle attack cooldown
    public virtual bool TryAttack(Vector3 aimPosition, GameObject instigator)
    {
        if(!this.enabled)  return false; 
        float nextAttackTime = _lastAttackTime + Cooldown;
        if (Time.time < nextAttackTime) return false;

        _lastAttackTime = Time.time;

        Attack(aimPosition, instigator);
        return true;
    }

    protected virtual void Attack(Vector3 aimPosition,  GameObject instigator)
    {
        // play SFX
        if (!AttackSFX.IsNull) RuntimeManager.PlayOneShot(AttackSFX, transform.position);
        if (!string.IsNullOrEmpty(AnimationTrigger)) Animator.SetTrigger(AnimationTrigger);

        // play VFX
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        WeaponEnabledEvent.Invoke(!this.gameObject.activeSelf);
        Functions.SetMouse(this.gameObject.activeSelf);
    }
}