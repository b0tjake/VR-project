using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

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
        IXRSelectInteractor interactor = args.interactorObject;
        XRInteractionManager interactionManager = grab.interactionManager;
        XRGrabInteractable pickupGrab = pickupObject.GetComponent<XRGrabInteractable>();

        // Show the pickup and put it where the visual ring/bracelet was
        pickupObject.SetActive(true);
        pickupObject.transform.SetPositionAndRotation(transform.position, transform.rotation);

        // Hand off the current selection from the visual to the real pickup so it
        // actually follows the hand instead of just being teleported and dropped.
        interactionManager.SelectExit(interactor, grab);

        // Hide the visual ring/bracelet
        gameObject.SetActive(false);

        interactionManager.SelectEnter(interactor, pickupGrab);
    }
}