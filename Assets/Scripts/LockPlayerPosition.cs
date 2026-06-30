using UnityEngine;

public class LockPlayerPosition : MonoBehaviour
{
    private Vector3 lockedPosition;

    private void Start()
    {
        lockedPosition = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = lockedPosition;
    }
}