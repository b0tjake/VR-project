using UnityEngine;
using UnityEngine.InputSystem;

public class RequiredMenu : MonoBehaviour
{
    [Header("References")]
    public Transform head;
    public GameObject currentCanvas;
    public TaskCanvasManager taskCanvas;


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

    public void ToggleMenu()
    {
        if (isOpen)
        {
            CloseCanvas();
        }
        else
        {
            OpenCanvas();

            taskCanvas.ToggleCurrentTask();
        }
    }

    public void OpenCanvas()
    {
        isOpen = true;

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

    public void CloseCanvas()
    {
        isOpen = false;
        currentCanvas.SetActive(false);
    }
}