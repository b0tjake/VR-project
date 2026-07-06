using UnityEngine;

public class ForkliftPath : MonoBehaviour
{
    [Header("Path")]
    public Transform[] points;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float turnSpeed = 60f;
    public float pointDistance = 0.5f;
    public bool loop = true;

    [Header("Wheels")]
    public Transform[] wheels;
    public float wheelRadius = 0.35f;

    [Header("Safety")]
    public AudioSource horn;

    private int currentPoint = 0;
    private bool canMove = true;

    void Update()
    {
        if (!canMove)
            return;

        if (points.Length == 0)
            return;

        Transform target = points[currentPoint];

        // Direction to target
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        // Distance
        float distance = direction.magnitude;

        if (distance < pointDistance)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
            {
                if (loop)
                    currentPoint = 0;
                else
                {
                    enabled = false;
                    return;
                }
            }

            target = points[currentPoint];
            direction = target.position - transform.position;
            direction.y = 0;
        }

        // Rotate
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime);

        // Move only when facing waypoint
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle < 45f)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            // Rotate wheels
            float wheelRotation =
                (moveSpeed / (2f * Mathf.PI * wheelRadius))
                * 360f * Time.deltaTime;

            foreach (Transform wheel in wheels)
            {
                if (wheel != null)
                    wheel.Rotate(Vector3.right, wheelRotation, Space.Self);
            }
        }
    }

    public void StopForklift()
    {
        canMove = false;

        if (horn != null && !horn.isPlaying)
            horn.Play();
    }

    public void ContinueForklift()
    {
        canMove = true;

        if (horn != null && horn.isPlaying)
            horn.Stop();
    }
}