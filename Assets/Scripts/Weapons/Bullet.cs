using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TDTK;
using UnityEngine;
using FMODUnity;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Collider _collider;
    public PlayerControllerFPSTD Player {  get; set; }
    [field: SerializeField] public float lifetime { get; protected set; } = 3f;
    [SerializeField] private bool UseGravity;
    [field: SerializeField, BoxGroup("SFX")] public EventReference BulletDestroyedSFX { get; protected set; }
    [field: SerializeField, BoxGroup("SFX")] public EventReference EnemyHitSFX { get; protected set; }
    [field: SerializeField, BoxGroup("VFX")] public VisualEffect BulletDestroyedVFX { get; protected set; }
    [field: SerializeField, BoxGroup("VFX")] public VisualEffect EnemyHitVFX { get; protected set; }

    [SerializeField] AudioClip enemyHitAudioClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = UseGravity;
        
        // Continuous Dynamic checks collisions more frequently to stop fast objects from passing through colliders
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Weapon>() || other.gameObject.GetComponent<Bullet>()) return;
        if (!other.gameObject.TryGetComponent(out UnitCreep creep))
        {
            //SFX and other effects when not hitting enemy
            if (!BulletDestroyedSFX.IsNull) RuntimeManager.PlayOneShot(BulletDestroyedSFX, transform.position);
            if (BulletDestroyedVFX != null) BulletDestroyedVFX.Play();
            Cleanup();
            return;
        }
        HitEnemy(creep);
    }

    private void HitEnemy(UnitCreep creep)
    {
        //SFX and other effects when hitting enemy
        Debug.Log($"Hit enemy {creep.gameObject.name}");
        if (!EnemyHitSFX.IsNull) RuntimeManager.PlayOneShot(EnemyHitSFX, transform.position);

        creep.ApplyEffect(Effect_DB.GetPrefab(8)); // 8 is 3-second stun. Effects are defined in Resources/DB/EffectDB. Really needs a better system.  --djoe 7/2024
        
        if (enemyHitAudioClip != null)
            AudioSource.PlayClipAtPoint(enemyHitAudioClip, transform.position);
        if (EnemyHitVFX != null) EnemyHitVFX.Play();
        Cleanup();
    }

    private void Cleanup()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        Destroy(gameObject,lifetime);
    }
}
