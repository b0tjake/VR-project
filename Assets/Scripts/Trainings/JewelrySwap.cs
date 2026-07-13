using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class JewelrySwap : MonoBehaviour
{
    public GameObject pickupObject;

    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Show the pickup
        pickupObject.SetActive(true);

        // Put it where the visual ring was
        pickupObject.transform.position = transform.position;
        pickupObject.transform.rotation = transform.rotation;

        // Hide the visual ring
        gameObject.SetActive(false);
    }
}