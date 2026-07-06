using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public AudioSource warningSound;
    public TrainingManager trainingManager;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!warningSound.isPlaying)
            trainingManager.LeftSafeZone();
                warningSound.Play();

            Debug.Log("EXIT : " + other.name);

        }
        Debug.Log("EXIT 2: " + other.name);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            warningSound.Stop();
            trainingManager.EnteredSafeZone();
             Debug.Log("ENTER : " + other.name);
             
        }
        Debug.Log("ENTER : 2" + other.name);

    }
}