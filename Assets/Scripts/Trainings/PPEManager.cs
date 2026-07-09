using UnityEngine;

public class PPEManager : MonoBehaviour
{
    public static PPEManager Instance;

    [Header("Equipped PPE")]
    public bool Helmet;
    public bool Vest;
    public bool LGlove;
    public bool RGlove;
    public bool LShoe;
    public bool RShoe;
    public bool WeldingMask;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool IsFullyEquipped()
    {
        return Helmet &&
               Vest &&
               LGlove &&
               RGlove &&
               LShoe &&
               RShoe &&
               WeldingMask;
    }
}