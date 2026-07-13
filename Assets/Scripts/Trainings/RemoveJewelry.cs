using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class RemoveJewelry : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        rb.isKinematic = true;
        rb.useGravity = false;

        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Parent before: " + (transform.parent ? transform.parent.name : "NULL"));

        transform.SetParent(null, true);

        Debug.Log("Parent after: " + (transform.parent ? transform.parent.name : "NULL"));

        rb.isKinematic = false;
        rb.useGravity = true;
    }
}