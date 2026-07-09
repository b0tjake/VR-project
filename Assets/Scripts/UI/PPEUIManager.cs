using UnityEngine;
using DG.Tweening;

public class PPEUIManager : MonoBehaviour
{
    public GameObject helmetUnchecked;
    public GameObject helmetChecked;

    public GameObject vestUnchecked;
    public GameObject vestChecked;

    public GameObject leftShoeUnchecked;
    public GameObject leftShoeChecked;

    public GameObject rightShoeUnchecked;
    public GameObject rightShoeChecked;

    public GameObject leftGloveUnchecked;
    public GameObject leftGloveChecked;

    public GameObject rightGloveUnchecked;
    public GameObject rightGloveChecked;

    public GameObject weldingMaskUnchecked;
    public GameObject weldingMaskChecked;

    void ShowChecked(GameObject uncheckedObj, GameObject checkedObj)
    {
        uncheckedObj.SetActive(false);
        checkedObj.SetActive(true);

        checkedObj.transform.localScale = Vector3.zero;
        checkedObj.transform.DOScale(Vector3.one, 0.25f)
            .SetEase(Ease.OutBack);
    }

    public void EquipHelmet()
    {
        ShowChecked(helmetUnchecked, helmetChecked);
    }

    public void EquipVest()
    {
        ShowChecked(vestUnchecked, vestChecked);
    }

    public void EquipLeftShoe()
    {
        ShowChecked(leftShoeUnchecked, leftShoeChecked);
    }

    public void EquipRightShoe()
    {
        ShowChecked(rightShoeUnchecked, rightShoeChecked);
    }

    public void EquipLeftGlove()
    {
        ShowChecked(leftGloveUnchecked, leftGloveChecked);
    }

    public void EquipRightGlove()
    {
        ShowChecked(rightGloveUnchecked, rightGloveChecked);
    }

    public void EquipWeldingMask()
    {
        ShowChecked(weldingMaskUnchecked, weldingMaskChecked);
    }
}