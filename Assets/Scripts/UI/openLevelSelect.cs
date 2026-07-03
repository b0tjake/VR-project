using UnityEngine;

public class OpenLevelSelect : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject levelSelectCanvas;
    public VRMenu vrMenu;

    public void OpenMenu()
    {
        vrMenu.ShowCanvas(levelSelectCanvas);
    }

    public void Back()
    {
        vrMenu.ShowCanvas(mainMenuCanvas);
    }
}