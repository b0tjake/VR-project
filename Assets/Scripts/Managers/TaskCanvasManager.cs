using UnityEngine;

public class TaskCanvasManager : MonoBehaviour
{
    public GameObject ppePanel;
    public GameObject weldingPanel;

    bool ppeCompleted = false;

    void Start()
    {
        ShowPPE();
    }

    public void PPEFinished()
    {
        ppeCompleted = true;
        ShowWelding();
    }

    public void ShowPPE()
    {
        ppePanel.SetActive(true);
        weldingPanel.SetActive(false);
    }

    public void ShowWelding()
    {
        ppePanel.SetActive(false);
        weldingPanel.SetActive(true);
    }

    public void ToggleCurrentTask()
    {
        if (ppeCompleted)
            ShowWelding();
        else
            ShowPPE();
    }
}