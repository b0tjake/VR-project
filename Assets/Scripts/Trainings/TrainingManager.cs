using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;


public class TrainingManager : MonoBehaviour
{
    [Header("Scene 1")]
    public TrainingUIManager ui;

    bool outside = false;
    Coroutine restartRoutine;

    [Header("Final Completion")]
    public GameObject completedPanel;
    public GameObject diactivateWhenWin;

    [Header("Scene 2 - PPE")]
    public TaskCanvasManager taskCanvas;
    public GameObject weldingArea;
    public GameObject bracelet;
    public GameObject ring;

    [Header("Locomotion")]
    public ContinuousMoveProvider moveProvider;
    public TeleportationProvider teleportProvider;
    public SnapTurnProvider snapTurnProvider;

    [Header("Wet Floor")]

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public XRGrabInteractable mopGrab;
    public int signsPlaced = 0;
    public int requiredSigns = 2;

    //======================================================
    // SAFE WALKWAY
    //======================================================

    private void Start()
    {
        if (moveProvider != null)
            moveProvider.enabled = true;

        if (teleportProvider != null)
            teleportProvider.enabled = true;
        if (mopGrab != null)
            mopGrab.enabled = false;

        SafeZone.ResetCounter();

        ui.ShowEmergencyUI();

    }

    public void LeftSafeZone()
    {
        if (outside) return;

        outside = true;

        ui.SetUnsafe();

        ui.ShowWarning(
            "You have left the safe walkway.\nPlease return immediately."
        );

        restartRoutine = StartCoroutine(RestartTimer());
    }

    public void EnteredSafeZone()
    {
        outside = false;

        ui.SetSafe();

        ui.HideWarning();

        if (restartRoutine != null)
            StopCoroutine(restartRoutine);
    }

    IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(5f);

        if (!outside)
            yield break;

        ui.HideWarning();

        ui.ShowRestart("Restarting...");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //======================================================
    // PPE FINISHED
    //======================================================

    public void PPECompleted()
    {
        Debug.Log("PPE Completed!");

        taskCanvas.PPEFinished();

        if (weldingArea != null && ring != null && bracelet != null)
        {
            weldingArea.SetActive(true);
            ring.SetActive(true);
            bracelet.SetActive(true);
        }
    }

    //======================================================
    // TRAINING FINISHED
    //======================================================

    public void TrainingCompleted()
    {
        Debug.Log("TRAINING COMPLETED!");

        if (moveProvider != null)
            moveProvider.enabled = false;

        if (teleportProvider != null)
            teleportProvider.enabled = false;


        if (diactivateWhenWin != null)
            diactivateWhenWin.SetActive(false);

        if (completedPanel != null)
            completedPanel.SetActive(true);
    }

    public void SignPlaced()
    {
        signsPlaced++;

        if (signsPlaced < requiredSigns)
            return;


        titleText.text = "CLEAN THE SPILL";

        descriptionText.text =
            "Retrieve the mop.\n\nClean the spill until the area is safe.";

        if (mopGrab != null)
            mopGrab.enabled = true;

        ui.ShowEmergencyUI();
    }
    public void SpillCleaned()
    {

        TrainingCompleted();
    }


}