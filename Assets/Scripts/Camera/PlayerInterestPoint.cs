using UnityEngine;

public class PlayerInterestPoint : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float smoothingTime;
    [SerializeField] private Rigidbody2D playerRigidBody;

    private Vector3 currentVelocity;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerRigidBody.position + playerRigidBody.velocity * offset, ref currentVelocity, smoothingTime);
    }
}