using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlayerEntity : MonoBehaviour
{
    private Rigidbody   _rb;
    private BoxCollider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        _collider = GetComponent<BoxCollider>();
        _collider.size   += new Vector3(0, 0, 5);
        _collider.center += new Vector3(0, 0, 2.5f);
    }
}
