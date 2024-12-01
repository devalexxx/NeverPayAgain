using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerEnterEvent : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter = new();

    void OnTriggerEnter(Collider collider)
    {
        onTriggerEnter?.Invoke(collider);
    }
}
