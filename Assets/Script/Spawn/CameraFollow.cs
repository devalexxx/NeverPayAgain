using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Transform du personnage à suivre
    public Vector3 offset = new Vector3(0, 5, -7); // Position relative de la caméra (au-dessus et derrière)
    public float followSpeed = 0.1f; // Vitesse de lissage du mouvement
    public float rotationSpeed = 5f; // Vitesse de rotation pour suivre le personnage
    public float zoomSpeed = 2f; // Vitesse de zoom avec la molette de la souris
    public float minZoom = -3f; // Distance minimale (zoom avant)
    public float maxZoom = -15f; // Distance maximale (zoom arrière)

    private float currentZoom; // Zoom actuel de la caméra

    void Start()
    {
        // Initialiser le zoom à la position Z de l'offset
        currentZoom = offset.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Gérer le zoom avec la molette de la souris
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom = Mathf.Clamp(currentZoom + scroll * zoomSpeed, maxZoom, minZoom);
        offset.z = currentZoom;

        // Calcul de la position souhaitée de la caméra
        Vector3 desiredPosition = target.position + offset;

        // Mouvement fluide vers la position souhaitée
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed);

        // Option : Faire pivoter la caméra pour qu'elle regarde toujours le personnage
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Option : Garder une rotation fixe pour une vue stable
        // transform.rotation = Quaternion.Euler(30, 0, 0);
     }
}

