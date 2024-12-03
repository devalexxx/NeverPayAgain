using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlayerEntity : MonoBehaviour
{
    private Rigidbody   _rb;
    private BoxCollider _collider;

    private List<Animator> _championAnimators;

    private float _speed;
    private bool  _isMoving;

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            if (value && !_isMoving)
            {
                _championAnimators.ForEach(an => an.SetBool("isWalking", true));
            }
            else if (!value && _isMoving)
            {
                _championAnimators.ForEach(an => an.SetBool("isWalking", false));
            }
            _isMoving = value;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        _collider = GetComponent<BoxCollider>();
        _collider.size   += new Vector3(0, 0, 5);
        _collider.center += new Vector3(0, 0, 2.5f);

        _championAnimators = new();
        foreach (Transform child in transform)
        {
            Transform tr = child.Find("Entity");
            if (tr != null)
            {   
                Animator an = tr.GetComponentInChildren<Animator>();
                if (an != null)
                {
                    _championAnimators.Add(an);
                }
            }
        }

        _speed    = 3.0f;
        _isMoving = false;
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }
}
