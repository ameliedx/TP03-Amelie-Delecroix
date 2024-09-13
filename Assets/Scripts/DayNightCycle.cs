using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;  // La lumière directionnelle (soleil)
    public float dayDuration = 60f; // Durée d'un jour en secondes (24 heures virtuelles)
    public float maxIntensity = 1f; // Intensité maximale de la lumière pendant la journée
    public float minIntensity = 0.2f; // Intensité minimale de la lumière pendant la nuit

    private float timeOfDay = 0f;   // Temps de la journée en pourcentage (0 à 1)

    void Update()
    {
        // Met à jour le temps de la journée en fonction du temps écoulé
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay >= 1f) // Si une journée est terminée, recommence le cycle
        {
            timeOfDay = 0f;
        }

        // Calculer la position du soleil en fonction du temps de la journée
        float sunAngle = timeOfDay * 360f; // L'angle du soleil dans le ciel (0 à 360 degrés)
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle - 90f, 0f, 0f);

        // Calculer l'intensité de la lumière en fonction du temps de la journée
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Cos(sunAngle * Mathf.Deg2Rad));
        directionalLight.intensity = intensity;
    }
}
