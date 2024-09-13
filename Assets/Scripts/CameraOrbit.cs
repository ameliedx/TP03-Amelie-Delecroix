using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;          // Le joueur que la cam�ra suit
    public float distance = 10.0f;    // Distance initiale de la cam�ra
    public float minDistance = 5.0f;  // Distance minimale de zoom
    public float maxDistance = 20.0f; // Distance maximale de zoom
    public float zoomSpeed = 2.0f;    // Vitesse de zoom
    public float rotationSpeed = 5.0f; // Vitesse de rotation autour du joueur

    public float minYRotation = -30f; // Limite inf�rieure de la rotation verticale
    public float maxYRotation = 60f;  // Limite sup�rieure de la rotation verticale

    private float currentX = 0.0f;    // Stockage de la rotation actuelle en X (horizontale)
    private float currentY = 0.0f;    // Stockage de la rotation actuelle en Y (verticale)
    private bool isRotating = false; // Indique si la cam�ra est en train de tourner avec clic droit

    void Update()
    {
        // G�re le zoom avec la molette de la souris
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // G�re la rotation libre avec le clic gauche
        if (Input.GetMouseButton(0)) // Clic gauche maintenu
        {
            // Rotation libre de la cam�ra autour du joueur
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, minYRotation, maxYRotation); // Limite la rotation verticale
        }

        // G�re la rotation autour du joueur avec le clic droit
        if (Input.GetMouseButton(1)) // Clic droit maintenu
        {
            isRotating = true;
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            // Rotation de la cam�ra autour du joueur
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 direction = rotation * Vector3.back * distance;
            transform.position = target.position + direction;
            transform.LookAt(target.position);

            // Rotation du joueur pour faire face � la cam�ra
            target.rotation = Quaternion.Euler(0, currentX, 0); // Le joueur fait face � la cam�ra
        }
        else
        {
            isRotating = false;
        }

        if (!isRotating)
        {
            // Normalement, la cam�ra suit le joueur � la distance sp�cifi�e
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 direction = new Vector3(0, 0, -distance);
            transform.position = target.position + rotation * direction;
            transform.LookAt(target.position);
        }
    }
}
