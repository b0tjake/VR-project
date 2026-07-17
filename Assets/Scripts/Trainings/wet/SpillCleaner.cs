using UnityEngine;

public class SpillCleaner : MonoBehaviour
{
    public GameObject spillObject;
    public ParticleSystem cleaningParticles;
    public TrainingManager trainingManager;

    public float requiredCleaningTime = 3f;

    private float cleaningProgress = 0f;
    private bool mopInside = false;
    private bool finished = false;

    private void Update()
    {
        if (finished)
            return;

        if (mopInside)
        {
            cleaningProgress += Time.deltaTime;

            if (cleaningProgress >= requiredCleaningTime)
            {
                finished = true;

                if (cleaningParticles != null)
                    cleaningParticles.Stop();

                spillObject.SetActive(false);

                if (trainingManager != null)
                    trainingManager.SpillCleaned();

                GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Mop"))
            return;

        mopInside = true;

        if (cleaningParticles != null)
        {
            cleaningParticles.Clear();
            cleaningParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Mop"))
            return;

        mopInside = false;

        if (cleaningParticles != null)
            cleaningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}