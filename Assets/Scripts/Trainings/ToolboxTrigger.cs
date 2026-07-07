using UnityEngine;

public class ToolboxTrigger : MonoBehaviour
{
    public TrainingManager   trainingManager;

    private bool hammerPlaced = false;
    private bool crowbarPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            hammerPlaced = true;
            Debug.Log("Hammer placed.");
        }

        if (other.CompareTag("Crowbar"))
        {
            crowbarPlaced = true;
            Debug.Log("Crowbar placed.");
        }

        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (hammerPlaced && crowbarPlaced)
        {
            Debug.Log("Training Complete!");

            if (trainingManager != null)
                trainingManager.TrainingCompleted();
        }
    }
}