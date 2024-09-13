using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du monstre
    public GameObject player; // Référence au joueur
    public float stopDistance = 1f; // Distance à laquelle le monstre s'arrête
    public int attackDamage = 10; // Dégâts infligés au joueur

    private Animator animator; // Référence à l'Animator
    private Rigidbody rb; // Référence au Rigidbody du monstre
    private bool isMovingTowardsPlayer = false; // Indique si le monstre se déplace vers le joueur

    private int currentHealth = 100; // Points de vie du monstre
    public UnityEngine.UI.Image bar;
    public int maxHealth = 100;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Verrouille la rotation sur les axes X et Z pour garder le monstre droit
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        animator.SetBool("isMoving", false);

    }

    void Update()
    {
        // Si le monstre doit se déplacer vers le joueur, il appelle la fonction de déplacement
        if (isMovingTowardsPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    // Détecte lorsque le joueur entre dans la zone de surveillance (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMovingTowardsPlayer = true; // Commence à se déplacer vers le joueur
        }
    }

    // Détecte lorsque le joueur quitte la zone de surveillance
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMovingTowardsPlayer = false;
            animator.SetBool("isAttacking", false);
        }
    }

    // Fonction pour déplacer le monstre vers le joueur
    void MoveTowardsPlayer()
    {
        // Calcule la direction vers le joueur en ignorant l'axe Y
        Vector3 direction = (new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position).normalized;

        animator.SetBool("isMoving", true); // Active l'animation de déplacement

        // Déplace le monstre vers le joueur en conservant la position Y
        Vector3 moveDirection = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDirection);

        // Oriente le monstre vers le joueur
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(lookRotation);

        // Vérifie si le monstre est assez proche pour attaquer
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= stopDistance)
        {
            StartAttack(); // Lance l'attaque
        }
    }

    void StopMoving()
    {
        animator.SetBool("isMoving", false); // Désactive l'animation de déplacement
        isMovingTowardsPlayer = false; // Arrête le mouvement
    }

    void StartAttack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            animator.SetBool("isAttacking", true);
            playerHealth.TakeDamage(attackDamage);
        }
    }



    // Fonction appelée lors de la collision avec le joueur
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartAttack(); // Lance l'attaque
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vie restante du Monstre: " + currentHealth);
        UpdateHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        StopMoving();
        WaitAndEndGame();
       
    }
    public void UpdateHealthBar(int value)
    {
        bar.fillAmount = (float)value / maxHealth;
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
