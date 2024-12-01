using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    private bool shouldMove = true;

    private Animator animator; // Référence à l'Animator

    void Start()
    {
        // Récupérer le composant Animator attaché
        animator = GetComponent<Animator>();

        // Assurez-vous que l'animation de marche est active au démarrage
        if (animator != null)
        {
            animator.SetBool("isWalking", shouldMove);
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            // Déplacement en ligne droite sur l'axe Z
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Activer l'animation de marche
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            // Arrêter l'animation de marche
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Arrête le déplacement lorsqu'un ennemi est rencontré
            shouldMove = false;

            // Arrêter l'animation de marche
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }

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
