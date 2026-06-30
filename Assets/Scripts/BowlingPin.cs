using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public bool IsKnockedDown { get; private set; }

    [Header("Knockdown settings")]
    public float knockdownAngle = 60f;

    private Quaternion startRotation;
    private Vector3 startUp;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startRotation = transform.rotation;
        startUp = transform.up;
    }

    private void Update()
    {
        float tiltAngle = Vector3.Angle(transform.up, startUp);

        IsKnockedDown = tiltAngle > knockdownAngle;
    }

    public void ResetPin()
    {
        IsKnockedDown = false;

        transform.rotation = startRotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
    }
}