using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowlingBallThrowAssist : MonoBehaviour
{
    [Header("Throw settings")]
    public float forwardForce = 60f;
    public float upwardForce = 0.5f;

    [Tooltip("Fallback direction used if controller direction cannot be detected.")]
    public Vector3 laneDirection = Vector3.forward;

    [Header("Direction settings")]
    public bool useControllerDirection = true;

    [Header("References")]
    public Rigidbody rb;
    public XRGrabInteractable grabInteractable;
    public BowlingGameManager gameManager;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (rb == null)
            return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 direction = laneDirection.normalized;

        if (useControllerDirection && args.interactorObject != null)
        {
            Transform interactorTransform = args.interactorObject.transform;

            direction = interactorTransform.forward;

            // Ignore vertical aiming, so the ball rolls along the lane instead of flying upward/downward.
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
            {
                direction = laneDirection.normalized;
            }
            else
            {
                direction.Normalize();
            }
        }

        Vector3 force = direction * forwardForce + Vector3.up * upwardForce;

        rb.AddForce(force, ForceMode.Impulse);

        Debug.Log("Ball released. Throw direction: " + direction);

        if (gameManager != null)
        {
            gameManager.CheckScoreDelayed();
        }
        else
        {
            Debug.LogWarning("GameManager is not assigned in BowlingBallThrowAssist.");
        }
    }
}