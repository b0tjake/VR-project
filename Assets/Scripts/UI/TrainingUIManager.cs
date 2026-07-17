using UnityEngine;
using TMPro;

public class TrainingUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject warningPanel;
    public GameObject restartPanel;
    public GameObject emergencyPanel;

    [Header("Safety")]
    public TMP_Text statusText;

    [Header("Warning")]
    public TMP_Text warningDescription;

    [Header("Restart")]
    public TMP_Text restartReason;




    public AudioSource alarmSound;

    //==================================================
    // SAFETY
    //==================================================

    public void SetSafe()
    {
        statusText.text = "<color=green>🟢 SAFE</color>";
    }

    public void SetUnsafe()
    {
        statusText.text = "<color=red>🔴 UNSAFE</color>";
    }

    //==================================================
    // EMERGENCY
    //==================================================

    public void ShowEmergencyUI()
    {
        CancelInvoke(nameof(HideEmergencyUI));

        Transform cam = Camera.main.transform;

        emergencyPanel.transform.position =
            cam.position + cam.forward * 2f;

        emergencyPanel.transform.forward = cam.forward;

        emergencyPanel.SetActive(true);

        if (alarmSound != null)
            alarmSound.Play();

        Invoke(nameof(HideEmergencyUI), 5f);
    }
    public void HideEmergencyUI()
    {
        emergencyPanel.SetActive(false);

        if (alarmSound != null)
            alarmSound.Stop();
    }
    //==================================================
    // WARNING
    //==================================================

    public void ShowWarning(string message)
    {
        warningDescription.text = message;
        warningPanel.SetActive(true);
    }

    public void HideWarning()
    {
        warningPanel.SetActive(false);
    }

    //==================================================
    // RESTART
    //==================================================

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