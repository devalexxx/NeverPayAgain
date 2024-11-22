using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Liste des points de spawn
    public GameObject playerPrefab; // Préfabriqué du joueur
    //public GameObject enemyPrefab; // Préfabriqué des ennemis

    void Start()
    {
        SpawnPlayer();
       // SpawnEnemies();
    }

    void SpawnPlayer()
    {
        if (spawnPoints.Length > 0 && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoints[0].position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("PlayerPrefab ou SpawnPoints est manquant !");
        }
    }

   /* void SpawnEnemies()
    {
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
   */

}