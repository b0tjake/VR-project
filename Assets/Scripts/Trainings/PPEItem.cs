using UnityEngine;

public class PPEItem : MonoBehaviour
{
    public enum PPEType
    {
        Helmet,
        Vest,
        LGlove,
        RGlove,
        LShoe,
        RShoe,
        WeldingMask
    }

    public PPEType ppeType;
}