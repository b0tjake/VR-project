using UnityEngine;

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



    public void EquipHelmet()
    {
        Debug.Log("Helmet UI Updated!");
        helmetUnchecked.SetActive(false);
        helmetChecked.SetActive(true);
    }

    public void EquipVest()
    {
        vestUnchecked.SetActive(false);
        vestChecked.SetActive(true);
    }

    public void EquipLeftShoe()
    {
        leftShoeUnchecked.SetActive(false);
        leftShoeChecked.SetActive(true);
    }

    public void EquipRightShoe()
    {
        rightShoeUnchecked.SetActive(false);
        rightShoeChecked.SetActive(true);
    }

    public void EquipLeftGlove()
    {
        leftGloveUnchecked.SetActive(false);
        leftGloveChecked.SetActive(true);
    }

    public void EquipRightGlove()
    {
        rightGloveUnchecked.SetActive(false);
        rightGloveChecked.SetActive(true);
    }

    public void EquipWeldingMask()
    {
        weldingMaskUnchecked.SetActive(false);
        weldingMaskChecked.SetActive(true);
    }
}