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
    [field: SerializeField, BoxGroup("Weapon")] protected Transform _muzzle;
    [field: SerializeField, BoxGroup("Weapon")] public BoolEventAsset WeaponEnabledEvent { get; protected set; }
    [field: SerializeField, BoxGroup("Weapon")] public float Range { get; protected set; } = 5f;
    [field: SerializeField, BoxGroup("SFX")] public EventReference FireBulletSFX { get; protected set; }
    [field: SerializeField, BoxGroup("Animation")] public Animator Animator { get; protected set; }
    [field: SerializeField, BoxGroup("Animation")] public string AnimationTrigger { get; protected set; }
    protected GameObject ChargingCrosshair;




    public void SetChargeCrosshair(GameObject crosshair)
    {
        ChargingCrosshair = crosshair;
    }

    protected virtual void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        WeaponEnabledEvent.Invoke(!this.gameObject.activeSelf);
        Functions.SetMouse(this.gameObject.activeSelf);    
    }

    public virtual void CalculateBulletDirection() { }
}