using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody; 
        if (rb != null)
        {

            Vector3 v = rb.linearVelocity;
            v.y = 0f;


            rb.linearVelocity = v + Vector3.up * launchForce;
        }
    }
}
