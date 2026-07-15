using UnityEngine;

public class PPEEquipZone1 : MonoBehaviour
{
    public PPEUIManager uiManager;

    public GameObject bodyHook;

    public TrainingManager trainingManager;

    private void OnTriggerEnter(Collider other)
    {
        PPEItem1 item = other.GetComponent<PPEItem1>();

        if (item == null)
            return;

        switch (item.ppeType)
        {
            case PPEItem1.PPEType.Helmet:
                if (HeightPPEManager.Instance.Helmet)
                    return;
                HeightPPEManager.Instance.Helmet = true;
                if (uiManager != null) uiManager.EquipHelmet();
                break;

            case PPEItem1.PPEType.Vest:
                if (HeightPPEManager.Instance.Vest)
                    return;
                HeightPPEManager.Instance.Vest = true;
                if (uiManager != null) uiManager.EquipVest();
                break;

            case PPEItem1.PPEType.LGlove:
                if (HeightPPEManager.Instance.LGlove)
                    return;
                HeightPPEManager.Instance.LGlove = true;
                if (uiManager != null) uiManager.EquipLeftGlove();
                break;

            // Added missing RGlove case
            case PPEItem1.PPEType.RGlove:
                if (HeightPPEManager.Instance.RGlove)
                    return;
                HeightPPEManager.Instance.RGlove = true;
                if (uiManager != null) uiManager.EquipRightGlove();
                break;

            case PPEItem1.PPEType.LShoe:
                if (HeightPPEManager.Instance.LShoe)
                    return;
                HeightPPEManager.Instance.LShoe = true;
                if (uiManager != null) uiManager.EquipLeftShoe();
                break;

            // Added missing RShoe case
            case PPEItem1.PPEType.RShoe:
                if (HeightPPEManager.Instance.RShoe)
                    return;
                HeightPPEManager.Instance.RShoe = true;
                if (uiManager != null) uiManager.EquipRightShoe();
                break;

            case PPEItem1.PPEType.Harness:
                if (HeightPPEManager.Instance.Harness)
                    return;
                HeightPPEManager.Instance.Harness = true;
                if (uiManager != null) uiManager.EquipWeldingMask();
                if (bodyHook != null)
                    bodyHook.SetActive(true);
                break;
        }

Debug.Log(item.ppeType + " Equipped");

other.gameObject.SetActive(false);

if (HeightPPEManager.Instance.IsFullyEquipped())
{
    Debug.Log("All PPE Equipped!");

    if (trainingManager != null)
        trainingManager.PPECompleted();

        }
    }
}