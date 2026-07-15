using UnityEngine;

public class ScaffoldTeleport : MonoBehaviour
{
    public Transform destination;
    public Transform xrOrigin;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name);

        if (!other.CompareTag("MainCamera"))
            return;

        Debug.Log("Teleport!");

        Vector3 offset = xrOrigin.position - Camera.main.transform.position;
        xrOrigin.position = destination.position + offset;
    }
}