using UnityEngine;

public class DashChecker : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public float GetDashDistance(float maxDistance)
    {
        for (var distance = maxDistance; distance > 0; distance -= 0.5f)
        {
            var t = transform;
            var hit = Physics2D.Raycast(t.position, t.right, distance, layerMask);
            if (hit.collider == null) return distance;
        }

        return 0;
    }
}