using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// FMOD namespaces:
using FMOD.Studio;
using FMODUnity;
using TDTK;

public class Weapon : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Weapon")] public float Damage { get; protected set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public DamageType DamageType { get; protected set; } = DamageType.Physical;
    [field: SerializeField, BoxGroup("Weapon")] public float Range { get; protected set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public float EffectiveRange { get; protected set; } = 4f;
    [field: SerializeField, BoxGroup("Weapon")] public float Cooldown { get; protected set; } = 0.5f;
    [field: SerializeField, BoxGroup("Weapon")] public float Duration { get; protected set; } = 1f;

    [field: SerializeField, BoxGroup("Animation")] public Animator Animator { get; protected set; }
    [field: SerializeField, BoxGroup("Animation")] public string AnimationTrigger { get; protected set; }

    [field: SerializeField, BoxGroup("SFX")] public EventReference AttackSFX { get; protected set; }

    [SerializeField, BoxGroup("UI Stuff")] private GameObject _buildButton;
    [SerializeField, BoxGroup("UI Stuff")] private GameObject _towerSelect;
    [SerializeField, BoxGroup("UI Stuff")] private GameObject _rangeIndicator;
    [SerializeField, BoxGroup("UI Stuff")] private GameObject _nodeIndicator;
    // TODO: sound FX
    // TODO: VFX
    // TODO: animation

    private float _lastAttackTime;

    private void OnValidate()
    {
        if (Animator == null) Animator = GetComponentInParent<Animator>();
    }

    public bool TryAttack(Vector3 aimPosition, GameObject instigator)
    {
        if(!this.enabled)  return false; 
        // handle attack cooldown
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
        _buildButton.gameObject.SetActive(!this.gameObject.activeSelf);
        _towerSelect.gameObject.SetActive(!this.gameObject.activeSelf);
        _rangeIndicator.gameObject.SetActive(!this.gameObject.activeSelf);
        _nodeIndicator.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}