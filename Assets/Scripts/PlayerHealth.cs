using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Pour recharger la scène ou changer de scène si nécessaire

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;
    public UnityEngine.UI.Image bar;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthBar(int value)
    {
        bar.fillAmount = (float)value / maxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; 
        Debug.Log("Joueur a reçu " + amount + " dégâts, vie restante: " + currentHealth);
        UpdateHealthBar(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Joueur est mort !");
        WaitAndEndGame();

    }

    void WaitAndEndGame()
    {
        StartCoroutine(WaitCoroutine());
    }

    // Coroutine pour attendre 2 secondes
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(4f); // Attend 2 secondes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
