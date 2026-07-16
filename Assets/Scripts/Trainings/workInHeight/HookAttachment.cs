using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HookAttachment : MonoBehaviour
{
    public bool attached = false;
    public bool isHeld = false;

    private XRGrabInteractable grab;
    private Rigidbody rb;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Player grabs the hook
        grab.selectEntered.AddListener(OnGrabbed);

        // Track if the hook is currently held
        grab.selectEntered.AddListener(_ => isHeld = true);
        grab.selectExited.AddListener(_ => isHeld = false);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Hook grabbed!");
    }
    public void AttachTo(Transform anchor)
    {
        attached = true;

        if (grab.isSelected)
        {
            grab.interactionManager.SelectExit(
                grab.firstInteractorSelecting,
                grab);
        }

        transform.SetParent(anchor, true);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        rb.useGravity = false;

        grab.enabled = false;

        Debug.Log("New parent: " + transform.parent.name);
    }
}