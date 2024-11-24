using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    private bool shouldMove = true;

    void Update()
    {
        if (shouldMove)
        {
            // Déplacement en ligne droite sur l'axe Z
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Arrête le déplacement lorsqu'un ennemi est rencontré
            shouldMove = false;

            // Déclenche le combat
            Debug.Log("Combat déclenché avec : " + other.name);
            StartBattle(other.gameObject);
        }
    }

    void StartBattle(GameObject enemy)
    {
        // Ajoutez ici votre logique de combat
        Debug.Log("Début du combat avec : " + enemy.name);
    }
    

}