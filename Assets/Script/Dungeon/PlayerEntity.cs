using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Class responsible for managing the player character's movement and animation within the game world.
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlayerEntity : MonoBehaviour
{
    private Rigidbody   _rb;        // The Rigidbody component used to enable trigger collision (set to kinematic).
    private BoxCollider _collider;  // The BoxCollider component used for detecting collisions.

    // List of Animator components for each champion in the player entity.
    private List<Animator> _championAnimators;

    private float _speed;       // The speed of the player's movement.
    private bool  _isMoving;    // A flag indicating if the player can move.

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            // Trigger animation changes based on the player's movement state.
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
        // Loop through all child transforms to find the "Entity" GameObjects and their animators.
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
            // Move the player forward at the specified speed when the IsMoving flag is true.
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }
}
