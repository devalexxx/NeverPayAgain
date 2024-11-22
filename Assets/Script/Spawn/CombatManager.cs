using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public void StartCombat(GameObject player, GameObject Enemy)
    {
        // Logique pour démarrer une séquence de combat
        Debug.Log($"{player.name} commence un combat avec {Enemy.name}");

        // Exemple : Passez à une scène de combat ou déclenchez une animation
        // SceneManager.LoadScene("CombatScene");
    }
}