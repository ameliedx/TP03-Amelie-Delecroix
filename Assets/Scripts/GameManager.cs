using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI winText;
    public TMPro.TextMeshProUGUI gameOverText;    // Le texte de d�faite
    public PlayerHealth playerHealth; // Le script de sant� du joueur (ou toute autre logique pour d�terminer la victoire/d�faite)
    public Monster monster;      // R�f�rence au monstre (ou toute autre logique pour d�terminer la victoire)

    private bool gameEnded = false; // Pour �viter de r�p�ter la logique de fin de jeu

    void Start()
    {
        // Assure que les textes sont invisibles au d�but
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        // V�rifie si le joueur est mort
        if (!gameEnded && playerHealth.CurrentHealth <= 0)
        {
            GameOver();  // Le joueur a perdu
        }

        // V�rifie si le monstre est mort (ou tout autre condition de victoire)
        if (!gameEnded && monster.CurrentHealth <= 0)
        {
            Win();  // Le joueur a gagn�
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
