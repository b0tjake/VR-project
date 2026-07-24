using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Prevents a held object from pushing, lifting, or otherwise colliding with the player
/// while it is grabbed. CharacterController does not use the general physics engine to
/// resolve its movement, so Physics.IgnoreCollision() has no effect on it. Instead, this
/// moves the held object's colliders to a dedicated layer that the player's
/// CharacterController is configured to exclude, then restores the original layer on release.
/// </summary>
public class IgnorePlayerCollisionOnGrab : MonoBehaviour
{
    private const int HeldItemLayer = 6; // "HeldItem" layer, excluded from the player's CharacterController.

    public Collider playerCollider;

    private Collider[] objectColliders;
    private int[] originalLayers;
    private CharacterController playerCharacterController;
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        objectColliders = GetComponentsInChildren<Collider>();
        originalLayers = new int[objectColliders.Length];

        if (playerCollider != null)
            playerCharacterController = playerCollider.GetComponent<CharacterController>();

        if (playerCharacterController != null)
            playerCharacterController.excludeLayers |= 1 << HeldItemLayer;

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        for (int i = 0; i < objectColliders.Length; i++)
        {
            if (objectColliders[i] == null)
                continue;

            originalLayers[i] = objectColliders[i].gameObject.layer;
            objectColliders[i].gameObject.layer = HeldItemLayer;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        for (int i = 0; i < objectColliders.Length; i++)
        {
            if (objectColliders[i] == null)
                continue;

            objectColliders[i].gameObject.layer = originalLayers[i];
        }
    }
}
