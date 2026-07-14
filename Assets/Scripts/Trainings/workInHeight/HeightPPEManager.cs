using UnityEngine;

public class HeightPPEManager : MonoBehaviour
{
    public static HeightPPEManager Instance;

    [Header("Equipped PPE")]
    public bool Helmet;
    public bool Vest;
    public bool LGlove;
    public bool RGlove;
    public bool LShoe;
    public bool RShoe;
    public bool Harness;


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
               Harness;
    }

}