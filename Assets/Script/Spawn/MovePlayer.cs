using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement
    private bool shouldMove = true;

    void Update()
    {
        if (shouldMove)
        {
            // D�placement en ligne droite sur l'axe Z
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Arr�te le d�placement lorsqu'un ennemi est rencontr�
            shouldMove = false;

            // D�clenche le combat
            Debug.Log("Combat d�clench� avec : " + other.name);
            StartBattle(other.gameObject);
        }
    }

    void StartBattle(GameObject enemy)
    {
        // Ajoutez ici votre logique de combat
        Debug.Log("D�but du combat avec : " + enemy.name);
    }
    

}