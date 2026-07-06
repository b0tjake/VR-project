using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public AudioSource warningSound;
    public TrainingManager trainingManager;

    private static int safeZoneCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera"))
            return;

        safeZoneCount++;

        warningSound.Stop();
        trainingManager.EnteredSafeZone();

        Debug.Log("ENTER : " + other.name +
                  " | Zones = " + safeZoneCount);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera"))
            return;

        safeZoneCount--;

        if (safeZoneCount < 0)
            safeZoneCount = 0;

        if (safeZoneCount == 0)
        {
            if (!warningSound.isPlaying)
                warningSound.Play();

            trainingManager.LeftSafeZone();
        }

        Debug.Log("EXIT : " + other.name +
                  " | Zones = " + safeZoneCount);
    }
}