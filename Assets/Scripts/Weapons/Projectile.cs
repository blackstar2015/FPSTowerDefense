using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMovement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private GameObject _attachedTrail;

    public float Range { get; set; } = 10f;
    public float Damage { get; set; } = 5f;
    public float Speed { get; set; } = 25f;
    public DamageType DamageType { get; set; } = DamageType.Physical;
    public GameObject Instigator { get; set; }
    public int Team { get; set; }

    private Vector3 _spawnPosition;

    private void OnValidate()
    {
        // configure rigidbody
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        // Continuous Dynamic checks collisions more frequently to stop fast objects from passing through colliders
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // configure collider
        _collider = GetComponent<CapsuleCollider>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        // launch!
        _rigidbody.linearVelocity = transform.forward * Speed;
        _spawnPosition = transform.position;
    }

    private void Update()
    {
        float distanceTraveled = Vector3.Distance(transform.position, _spawnPosition);
        if (distanceTraveled > Range) Cleanup();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // regardless of what was hit, we clean up
        //Cleanup();
    }

    private void Cleanup()
    {
        Destroy(gameObject);

        // detach VFX trail and destroy with delay
        if (_attachedTrail != null)
        {
            _attachedTrail.transform.SetParent(null);
            Destroy(_attachedTrail, 2f);
        }
    }
}