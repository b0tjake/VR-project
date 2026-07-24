using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FireExtinguisher : MonoBehaviour
{
    // A single fire + its smoke. Detected as "being sprayed" when the water spray is aimed at fireObjects[0].
    [System.Serializable]
    public class FireTarget
    {
        [Tooltip("Fire GameObjects for this target. The first entry's position is used to detect if the spray is aimed at this fire.")]
        public GameObject[] fireObjects;
        [Tooltip("Smoke GameObjects for this target, shown while spraying and kept alive for a while after the fire is extinguished.")]
        public GameObject[] smokeObjects;
        [System.NonSerialized] public float smokeTimer;

        [System.NonSerialized] public float accumulatedSprayTime;
        [System.NonSerialized] public bool isExtinguished;
        [System.NonSerialized] public Coroutine smokeCoroutine;
    }

    [Header("References")]
    public ParticleSystem waterParticles;

    [Header("Spray Input")]
    [Tooltip("Spray action bound to the left hand controller's trigger.")]
    public InputActionReference sprayActionLeft;
    [Tooltip("Spray action bound to the right hand controller's trigger.")]
    public InputActionReference sprayActionRight;

    [Header("Spray Aim Detection")]
    [Tooltip("Maximum distance the spray can reach a fire target.")]
    public float sprayRange = 10f;
    [Tooltip("Half-angle (degrees) of the cone around the spray direction that still counts as aiming at a target.")]
    public float sprayConeHalfAngle = 25f;
    [Tooltip("Rotation (euler) applied on top of the water particle system's transform to match the cone shape's emission direction, matching the Shape module's Rotation.")]
    public Vector3 sprayDirectionOffsetEuler = new Vector3(55.62f, -99f, 328.25f);

    [Header("Fire Extinguishing")]
    [Tooltip("Each entry is one fire and its own smoke, extinguished independently based on which one is being sprayed.")]
    public List<FireTarget> fireTargets = new List<FireTarget>();
    [Tooltip("Total seconds of continuous spraying (aimed at a fire) required to extinguish it.")]
    public float sprayTimeToExtinguish = 4f;
    [Tooltip("Seconds the smoke stays visible after its fire is extinguished.")]
    public float smokeDuration = 15f;

    private const float SprayInputThreshold = 0.1f;

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;
    private InteractorHandedness holdingHandedness = InteractorHandedness.None;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnEnable()
    {
        if (sprayActionLeft != null)
            sprayActionLeft.action.Enable();

        if (sprayActionRight != null)
            sprayActionRight.action.Enable();
    }

    private void OnDisable()
    {
        if (sprayActionLeft != null)
            sprayActionLeft.action.Disable();

        if (sprayActionRight != null)
            sprayActionRight.action.Disable();
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        holdingHandedness = args.interactorObject.handedness;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        holdingHandedness = InteractorHandedness.None;

        if (waterParticles.isPlaying)
            waterParticles.Stop();
    }

    private void Update()
    {
        bool isSpraying = false;

        if (isHeld)
        {
            float sprayValue = ReadSprayValueForHoldingHand();
            isSpraying = sprayValue > SprayInputThreshold;

            if (isSpraying && !waterParticles.isPlaying)
                waterParticles.Play();
            else if (!isSpraying && waterParticles.isPlaying)
                waterParticles.Stop();
        }

        UpdateFireTargets(isSpraying);
    }

    // Reads the spray trigger value from whichever controller is currently holding the extinguisher.
    private float ReadSprayValueForHoldingHand()
    {
        InputActionReference activeAction = holdingHandedness == InteractorHandedness.Left
            ? sprayActionLeft
            : sprayActionRight;

        if (activeAction == null)
            return 0f;

        return activeAction.action.ReadValue<float>();
    }

    // Checks which fire the spray is currently aimed at and progresses/extinguishes it accordingly.
    private void UpdateFireTargets(bool isSpraying)
    {
        Vector3 sprayOrigin = waterParticles.transform.position;
        Vector3 sprayDirection = waterParticles.transform.rotation * Quaternion.Euler(sprayDirectionOffsetEuler) * Vector3.forward;

        foreach (FireTarget target in fireTargets)
        {
            if (target.isExtinguished)
                continue;

            bool isAimedAtTarget = isSpraying && IsAimedAtTarget(target, sprayOrigin, sprayDirection);

            if (isAimedAtTarget)
            {
                SetGameObjectsActive(target.smokeObjects, true);

                target.smokeTimer = 1.5f; // Keep smoke alive for 1.5 seconds

                target.accumulatedSprayTime += Time.deltaTime;

                if (target.accumulatedSprayTime >= sprayTimeToExtinguish)
                    ExtinguishTarget(target);
            }
            else
            {

                {
                    if (target.smokeTimer > 0f)
                    {
                        target.smokeTimer -= Time.deltaTime;
                    }
                    else
                    {
                        SetGameObjectsActive(target.smokeObjects, false);
                    }
                }
            }
        }
    }

    private bool IsAimedAtTarget(FireTarget target, Vector3 sprayOrigin, Vector3 sprayDirection)
    {
        if (target.fireObjects == null || target.fireObjects.Length == 0 || target.fireObjects[0] == null)
            return false;

        Vector3 toTarget = target.fireObjects[0].transform.position - sprayOrigin;
        float distance = toTarget.magnitude;

        if (distance > sprayRange)
            return false;

        float angle = Vector3.Angle(sprayDirection, toTarget);
        return angle <= sprayConeHalfAngle;
    }

    private void ExtinguishTarget(FireTarget target)
    {
        target.isExtinguished = true;

        SetGameObjectsActive(target.fireObjects, false);
        SetGameObjectsActive(target.smokeObjects, true);

        if (target.smokeCoroutine != null)
            StopCoroutine(target.smokeCoroutine);

        target.smokeCoroutine = StartCoroutine(HideSmokeAfterDelay(target));
    }

    private IEnumerator HideSmokeAfterDelay(FireTarget target)
    {
        yield return new WaitForSeconds(smokeDuration);

        SetGameObjectsActive(target.smokeObjects, false);
        target.smokeCoroutine = null;
    }

    // Returns true once every fire target has been extinguished. Used by other systems (e.g. the fire alarm) to gate on fire being fully out.
    public bool AreAllFiresExtinguished()
    {
        if (fireTargets == null || fireTargets.Count == 0)
            return false;

        foreach (FireTarget target in fireTargets)
        {
            if (!target.isExtinguished)
                return false;
        }

        return true;
    }

    private static void SetGameObjectsActive(GameObject[] gameObjects, bool active)
    {
        if (gameObjects == null)
            return;

        foreach (GameObject go in gameObjects)
        {
            if (go != null && go.activeSelf != active)
                go.SetActive(active);
        }
    }
}
