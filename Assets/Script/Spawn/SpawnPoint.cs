using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Choisissez une couleur
        Gizmos.DrawSphere(transform.position, 0.5f); // Taille du gizmo
    }
}