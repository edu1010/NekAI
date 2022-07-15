using UnityEngine;

public class RaycastDetector2D : MonoBehaviour
{
    [SerializeField] private Transform raycastSource;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float raycastDistance;
    [SerializeField] private bool drawRaycast;

    private void OnDrawGizmos()
    {
        if (!drawRaycast) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastSource.position, raycastSource.right * raycastDistance);
    }

    public (bool, float) GetDistanceToCollisionLayers()
    {
        var hit = GetRaycastHit();
        return hit.collider is null ? (false, 0f) : (true, hit.distance);
    }

    public bool IsRaycastCollision()
    {
        return GetRaycastHit().collider != null;
    }

    public RaycastHit2D GetRaycastHit()
    {
        return Physics2D.Raycast(raycastSource.position, raycastSource.right, raycastDistance, collisionLayers);
    }
}