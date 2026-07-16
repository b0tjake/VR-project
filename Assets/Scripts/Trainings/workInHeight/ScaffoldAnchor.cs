using UnityEngine;

public class ScaffoldAnchor : MonoBehaviour
{
    public TrainingManager trainingManager;

    private void OnTriggerEnter(Collider other)
    {
        HookAttachment hook = other.GetComponent<HookAttachment>();

        if (hook == null)
            return;

        if (!hook.isHeld)
            return;

        if (hook.attached)
            return;

        hook.AttachTo(transform);

        trainingManager.TrainingCompleted();
    }
}