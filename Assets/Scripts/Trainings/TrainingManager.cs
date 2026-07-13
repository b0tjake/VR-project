using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    //======================================================
    // SAFE WALKWAY
    //======================================================

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
        weldingArea.SetActive(true);
        ring.SetActive(true);
        bracelet.SetActive(true);
}

    //======================================================
    // TRAINING FINISHED
    //======================================================

    public void TrainingCompleted()
    {
        Debug.Log("TRAINING COMPLETED!");

        if (diactivateWhenWin != null)
            diactivateWhenWin.SetActive(false);

        if (completedPanel != null)
            completedPanel.SetActive(true);
    }
}