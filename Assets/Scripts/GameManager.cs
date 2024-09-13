using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI winText;
    public TMPro.TextMeshProUGUI gameOverText;    // Le texte de défaite
    public PlayerHealth playerHealth; // Le script de santé du joueur (ou toute autre logique pour déterminer la victoire/défaite)
    public Monster monster;      // Référence au monstre (ou toute autre logique pour déterminer la victoire)

    private bool gameEnded = false; // Pour éviter de répéter la logique de fin de jeu

    void Start()
    {
        // Assure que les textes sont invisibles au début
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Vérifie si le joueur est mort
        if (!gameEnded && playerHealth.CurrentHealth <= 0)
        {
            GameOver();  // Le joueur a perdu
        }

        // Vérifie si le monstre est mort (ou tout autre condition de victoire)
        if (!gameEnded && monster.CurrentHealth <= 0)
        {
            Win();  // Le joueur a gagné
        }
    }

    // Fonction pour afficher le Game Over
    void GameOver()
    {
        gameEnded = true;
        gameOverText.gameObject.SetActive(true); // Affiche le texte "Game Over"
        Debug.Log("Game Over!");  // Log de test
   



    }

    // Fonction pour afficher la victoire
    void Win()
    {
        gameEnded = true;
        winText.gameObject.SetActive(true); // Affiche le texte "Win"
        Debug.Log("You Win!");  // Log de test
        



    }
}
