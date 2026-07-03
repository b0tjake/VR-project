using UnityEngine;
using UnityEngine.InputSystem;

public class VRMenu : MonoBehaviour
{
    [Header("References")]
    public Transform head;
    public GameObject currentCanvas;

    [Header("Menu")]
    public float distance = 1.5f;

    [Header("Input")]
    public InputActionReference menuAction;

    private bool isOpen;

    private void OnEnable()
    {
        menuAction.action.Enable();
    }

    private void OnDisable()
    {
        menuAction.action.Disable();
    }

    void Update()
    {
        if (menuAction.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }
    }

    public void ShowCanvas(GameObject canvas)
    {
        // Hide previous menu if there is one
        if (currentCanvas != null)
            currentCanvas.SetActive(false);

        currentCanvas = canvas;

        Vector3 forward = head.forward;
        forward.y = 0;
        forward.Normalize();

        currentCanvas.transform.position =
            head.position + forward * distance;

        Vector3 dir = head.position - currentCanvas.transform.position;
        dir.y = 0;
        currentCanvas.transform.forward = -dir;

        currentCanvas.SetActive(true);

        isOpen = true;
    }

    void ToggleMenu()
    {
        if (currentCanvas == null)
            return;

        isOpen = !isOpen;

        if (isOpen)
        {
            Vector3 forward = head.forward;
            forward.y = 0;
            forward.Normalize();

            currentCanvas.transform.position =
                head.position + forward * distance;

            Vector3 dir = head.position - currentCanvas.transform.position;
            dir.y = 0;
            currentCanvas.transform.forward = -dir;

            currentCanvas.SetActive(true);
        }
        else
        {
            currentCanvas.SetActive(false);
        }
    }
}