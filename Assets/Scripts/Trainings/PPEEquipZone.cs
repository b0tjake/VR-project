using UnityEngine;

public class PPEEquipZone : MonoBehaviour
{
    public PPEUIManager uiManager;


    public TrainingManager trainingManager;

    private void OnTriggerEnter(Collider other)
    {
        PPEItem item = other.GetComponent<PPEItem>();

        if (item == null)
            return;

        switch (item.ppeType)
        {
            case PPEItem.PPEType.Helmet:
                if (PPEManager.Instance.Helmet)
                    return;
                PPEManager.Instance.Helmet = true;
                if (uiManager != null) uiManager.EquipHelmet();
                break;

            case PPEItem.PPEType.Vest:
                if (PPEManager.Instance.Vest)
                    return;
                PPEManager.Instance.Vest = true;
                if (uiManager != null) uiManager.EquipVest();
                break;

            case PPEItem.PPEType.LGlove:
                if (PPEManager.Instance.LGlove)
                    return;
                PPEManager.Instance.LGlove = true;
                if (uiManager != null) uiManager.EquipLeftGlove();
                break;

            // Added missing RGlove case
            case PPEItem.PPEType.RGlove:
                if (PPEManager.Instance.RGlove)
                    return;
                PPEManager.Instance.RGlove = true;
                if (uiManager != null) uiManager.EquipRightGlove();
                break;

            case PPEItem.PPEType.LShoe:
                if (PPEManager.Instance.LShoe)
                    return;
                PPEManager.Instance.LShoe = true;
                if (uiManager != null) uiManager.EquipLeftShoe();
                break;

            // Added missing RShoe case
            case PPEItem.PPEType.RShoe:
                if (PPEManager.Instance.RShoe)
                    return;
                PPEManager.Instance.RShoe = true;
                if (uiManager != null) uiManager.EquipRightShoe();
                break;

            case PPEItem.PPEType.WeldingMask:
                if (PPEManager.Instance.WeldingMask)
                    return;
                PPEManager.Instance.WeldingMask = true;
                if (uiManager != null) uiManager.EquipWeldingMask();
                break;
        }

Debug.Log(item.ppeType + " Equipped");

other.gameObject.SetActive(false);

if (PPEManager.Instance.IsFullyEquipped())
{
    Debug.Log("All PPE Equipped!");

    if (trainingManager != null)
        trainingManager.PPECompleted();

        }
    }
}