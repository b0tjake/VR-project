using UnityEngine;

public class FadeByDistance : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Distance")]
    public float appearDistance = 4f;
    public float disappearDistance = 6f;

    [Header("Fade")]
    public float fadeSpeed = 5f;

    private Material mat;
    private Color color;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        color = mat.color;

        color.a = 0f;
        mat.color = color;
    }

    void Update()
    {
        float d = Vector3.Distance(player.position, transform.position);

        float targetAlpha = 0f;

        if (d <= appearDistance)
            targetAlpha = 1f;
        else if (d < disappearDistance)
            targetAlpha = Mathf.InverseLerp(disappearDistance, appearDistance, d);

        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        mat.color = color;
    }
}