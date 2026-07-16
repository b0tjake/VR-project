using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadTrainingHub()
    {
        SceneManager.LoadScene("TrainingHub");
    }

    public void LoadSafeWalkways()
    {
        SceneManager.LoadScene("SafeWalkways");
    }

    public void LoadPPESelection()
    {
        SceneManager.LoadScene("PPESelection");
    }

    public void LoadWetFloor()
    {
        SceneManager.LoadScene("wetFloor");
    }

    public void LoadForkliftTraining()
    {
        SceneManager.LoadScene("ForkliftTraining");
    }

    public void LoadWorkingAtHeight()
    {
        SceneManager.LoadScene("WorkingAtHeight");
    }

    public void LoadFireEmergency()
    {
        SceneManager.LoadScene("FireEmergency");
    }

    public void LoadFinalAssessment()
    {
        SceneManager.LoadScene("FinalAssessment");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}