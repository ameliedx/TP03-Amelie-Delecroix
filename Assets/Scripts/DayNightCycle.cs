using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;  // La lumi�re directionnelle (soleil)
    public float dayDuration = 60f; // Dur�e d'un jour en secondes (24 heures virtuelles)
    public float maxIntensity = 1f; // Intensit� maximale de la lumi�re pendant la journ�e
    public float minIntensity = 0.2f; // Intensit� minimale de la lumi�re pendant la nuit

    private float timeOfDay = 0f;   // Temps de la journ�e en pourcentage (0 � 1)

    void Update()
    {
        // Met � jour le temps de la journ�e en fonction du temps �coul�
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay >= 1f) // Si une journ�e est termin�e, recommence le cycle
        {
            timeOfDay = 0f;
        }

        // Calculer la position du soleil en fonction du temps de la journ�e
        float sunAngle = timeOfDay * 360f; // L'angle du soleil dans le ciel (0 � 360 degr�s)
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle - 90f, 0f, 0f);

        // Calculer l'intensit� de la lumi�re en fonction du temps de la journ�e
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Cos(sunAngle * Mathf.Deg2Rad));
        directionalLight.intensity = intensity;
    }
}
