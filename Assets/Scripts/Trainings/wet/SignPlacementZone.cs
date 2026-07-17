using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SignPlacementZone : MonoBehaviour
{
    public TrainingManager trainingManager;

    [Header("Visual")]
    public MeshRenderer greenAreaRenderer;

    private bool placed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (placed)
            return;

        if (!other.CompareTag("WetFloorSign"))
            return;

        placed = true;

        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;

        // Hide the green placement area
        if (greenAreaRenderer != null)
            greenAreaRenderer.enabled = false;

        // Disable the trigger so it can't be triggered again
        GetComponent<Collider>().enabled = false;

        trainingManager.SignPlaced();
    }
}