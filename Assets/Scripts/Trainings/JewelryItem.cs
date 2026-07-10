using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class JewelryItem : MonoBehaviour
{
    public PPEManager manager;

    public enum JewelryType
    {
        Ring,
        Watch,
        Bracelet,
        Necklace
    }

    public JewelryType type;

    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectExited.AddListener(Removed);
    }

    void Removed(SelectExitEventArgs args)
    {
        switch (type)
        {
            case JewelryType.Ring:
                manager.ringRemoved = true;
                break;

            case JewelryType.Watch:
                manager.watchRemoved = true;
                break;

            case JewelryType.Bracelet:
                manager.braceletRemoved = true;
                break;

            case JewelryType.Necklace:
                manager.necklaceRemoved = true;
                break;
        }

        manager.CheckJewelry();
    }
}