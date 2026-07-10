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

        public bool ringRemoved;
        public bool watchRemoved;
        public bool braceletRemoved;
        public bool necklaceRemoved;

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

    public void CheckJewelry()
    {
        if (ringRemoved &&
            watchRemoved &&
            braceletRemoved &&
            necklaceRemoved)
        {
            Debug.Log("Jewelry removed!");

            // Tick the checklist
        }
    }

}