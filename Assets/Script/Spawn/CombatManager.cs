using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public void StartCombat(GameObject player, GameObject Enemy)
    {
        // Logique pour d�marrer une s�quence de combat
        Debug.Log($"{player.name} commence un combat avec {Enemy.name}");

        // Exemple : Passez � une sc�ne de combat ou d�clenchez une animation
        // SceneManager.LoadScene("CombatScene");
    }
}