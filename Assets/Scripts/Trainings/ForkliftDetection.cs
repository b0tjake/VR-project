using UnityEngine;

public class ForkliftDetection : MonoBehaviour
{
    public ForkliftPath forklift;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            forklift.StopForklift();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            forklift.ContinueForklift();
        }
    }
}