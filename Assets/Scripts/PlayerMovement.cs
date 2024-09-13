using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Vitesse de déplacement
    public float jumpForce = 7f;      // Force du saut

    private Rigidbody rb;
    private bool isGrounded;

    private Animator animator;

    public float attackRange = 2f; // Distance à laquelle l'attaque touche le monstre
    public int attackDamage = 10;  // Dégâts infligés par le joueur

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        // Verrouille la rotation sur les axes X et Z pour garder la capsule droite
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        animator.SetBool("isMoving", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFighting", false);
    }

    void Update()
    {
        Move();
        CheckGroundStatus();
        Jump();
        if (Input.GetKeyDown(KeyCode.V))
        {
            Attack();
        }
        else
        {
            animator.SetBool("isFighting", false);
        }

    }

    void Move()
    {
        // Récupère les entrées du clavier pour les axes X et Z
        float moveHorizontal = Input.GetAxis("Horizontal"); // A et D
        float moveVertical = Input.GetAxis("Vertical");     // W et S

        // Calcule la direction du mouvement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement.magnitude > 0)
        {
            // Déplacement du personnage
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

            // Tourne le personnage dans la direction du mouvement
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation); // Met à jour la rotation du Rigidbody pour qu'il fasse face à la direction de déplacement

            animator.SetBool("isMoving", true); // Active l'animation de mouvement
        }
        else
        {
            animator.SetBool("isMoving", false); // Active l'animation idle
        }

    }

    void CheckGroundStatus()
    {
        // Utilise un Raycast vers le bas pour vérifier si le joueur est au sol
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Vérifie si l'objet touché a le tag "Sol"
            if (hit.collider.CompareTag("Terrain"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        // Si la touche Espace est pressée et que le joueur est au sol, il saute
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false); // Active l'animation idle
        }
    }

    void Attack()
    {
        animator.SetBool("isFighting", true);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Monster"))
                {
                    Monster monster = hitCollider.GetComponent<Monster>();
                    if (monster != null)
                    {
                        monster.TakeDamage(attackDamage); // Inflige des dégâts au monstre
                        
                    }
                }
            }
        
    }
}
