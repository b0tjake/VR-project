using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainingManager : MonoBehaviour
{
    public TrainingUIManager ui;

    bool outside = false;
    Coroutine restartRoutine;

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

        ui.ShowRestart(
            "Restarting..."
        );

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}