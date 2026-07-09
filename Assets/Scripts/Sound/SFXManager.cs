using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource audioSource;

    public AudioClip equipSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayEquip()
    {
        audioSource.PlayOneShot(equipSound);
    }
}