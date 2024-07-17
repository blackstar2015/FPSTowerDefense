using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Collider _collider;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        // Continuous Dynamic checks collisions more frequently to stop fast objects from passing through colliders
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

}
