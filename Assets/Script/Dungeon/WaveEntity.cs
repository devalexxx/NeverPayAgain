using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for managing the wave's collider and its trigger functionality.
[RequireComponent(typeof(BoxCollider))]
public class WaveEntity : MonoBehaviour
{
    private void Awake()
    {
        // Set the BoxCollider's "isTrigger" property to true, enabling trigger functionality for collision detection.
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
