using UnityEngine;

// Plays the alarm sound on scene start and keeps it looping until the player clicks
// the fire alarm GameObject (trigger button select) after every fire has been extinguished.
[RequireComponent(typeof(AudioSource))]
public class FireAlarm : MonoBehaviour
{
    [Header("References")]
    public AudioSource alarmAudioSource;
    [Tooltip("The extinguisher whose fires must all be put out before the alarm can be silenced.")]
    public FireExtinguisher fireExtinguisher;
    public TrainingManager trainingManager;

    private void Awake()
    {
        if (alarmAudioSource == null)
            alarmAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        alarmAudioSource.loop = true;
        alarmAudioSource.Play();
    }

    // Hooked up to the XRSimpleInteractable's selectEntered event (trigger button click).
    public void TryStopAlarm()
    {
        if (fireExtinguisher != null && !fireExtinguisher.AreAllFiresExtinguished())
            return;

        if (alarmAudioSource.isPlaying)
            alarmAudioSource.Stop();

        if (trainingManager != null)
            trainingManager.TrainingCompleted();
    }
}
