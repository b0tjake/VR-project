using UnityEngine;
using TMPro;

public class TrainingUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject warningPanel;
    public GameObject restartPanel;

    [Header("Safety")]
    public TMP_Text statusText;

    [Header("Warning")]
    public TMP_Text warningDescription;

    [Header("Restart")]
    public TMP_Text restartReason;

    public void SetSafe()
    {
        statusText.text = "<color=green> 🟢 SAFE</color>";
    }

    public void SetUnsafe()
    {
        statusText.text = "<color=red>🔴 UNSAFE</color>";
    }

    public void ShowWarning(string message)
    {
        warningDescription.text = message;
        warningPanel.SetActive(true);
    }

    public void HideWarning()
    {
        warningPanel.SetActive(false);
    }

    public void ShowRestart(string reason)
    {
        restartReason.text = reason;
        restartPanel.SetActive(true);
    }

    public void HideRestart()
    {
        restartPanel.SetActive(false);
    }
}