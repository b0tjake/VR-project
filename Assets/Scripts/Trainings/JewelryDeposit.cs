using UnityEngine;

public class JewelryDeposit : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public TrainingManager trainingManager;

    private int deposited = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Jewelry"))
            return;

        deposited++;

        other.gameObject.SetActive(false);

        if (deposited >= 2)
        {
            foreach (GameObject obj in objectsToDisable)
                obj.SetActive(false);

            Debug.Log("Jewelry removed!");

            trainingManager.TrainingCompleted();
        }
    }
}