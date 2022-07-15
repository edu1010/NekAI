using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(0, 1)] private float movementSmoothing = 0.5f;
    [SerializeField] [Range(0, 1)] private float zoomSmoothing = 0.5f;
    [SerializeField] [Range(0, 1)] private float boundPercentage;
    [SerializeField] private float minZoomDistance = 10f;
    [SerializeField] private float maxZoomDistance = 50f;
    [SerializeField] private Transform bottomLeftLimit;
    [SerializeField] private Transform topRightLimit;
    [SerializeField] private float zoomLimiter;

    public List<PointOfInterest> PointsOfInterest;
    public float MinDistInterest = 3;
    public float MaxDistInterest = 8;
    private Bounds bounds;
    private Vector3 currentCameraPosition;
    private float positionZ;

    public List<Transform> Targets => targets;

    public void Awake()
    {
        _camera = GetComponent<Camera>();
        positionZ = transform.position.z;
    }

    public void FixedUpdate()
    {
        MoveCamera();
        AdjustPointOfInterest();
        FinalMove();
        ZoomCamera();
        
    }

    public void LateUpdate()
    {
        if (targets.Count == 0) return;
        bounds = new Bounds(targets[0].position, Vector3.zero);
        targets.ForEach(transform => bounds.Encapsulate(transform.position));
    }

    private void MoveCamera()
    {
        currentCameraPosition = targets.Count == 1 ? targets[0].position : bounds.center;
        currentCameraPosition += offset;
        if (bottomLeftLimit) currentCameraPosition = Vector3.Max(bottomLeftLimit.position, currentCameraPosition);
        if (topRightLimit) currentCameraPosition = Vector3.Min(topRightLimit.position, currentCameraPosition);
        currentCameraPosition = Vector3.Lerp(transform.position, currentCameraPosition, -(movementSmoothing - 1));
        currentCameraPosition.z = positionZ;
        //_camera.transform.position = currentCameraPosition;
    }
    private void FinalMove()
    {
        _camera.transform.position = currentCameraPosition;
    }

    private void ZoomCamera()
    {
        var size = Mathf.Max(bounds.size.y, bounds.size.x / _camera.aspect);
        size = Mathf.Clamp(size * (1 + boundPercentage), minZoomDistance, maxZoomDistance);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, size, -(zoomSmoothing - 1));
    }

    void AdjustPointOfInterest()
    {
        for (int i = 0; i < PointsOfInterest.Count; i++)
        {
            if (IsInRange(PointsOfInterest[i].transform.position))
            {
                Vector3 dir = (PointsOfInterest[i].transform.position - currentCameraPosition).normalized;
                dir.z = 0;
                currentCameraPosition += dir * PointsOfInterest[i].Weight * DistanceModifier(PointsOfInterest[i].transform.position);
            }
        }

    }
    private float DistanceModifier(Vector3 position)
    {
        float dist = Vector2.Distance(position, currentCameraPosition);
        float peak = MinDistInterest + (MaxDistInterest - MinDistInterest) / 2;
        return Mathf.Clamp01(1 / Mathf.Pow(dist - peak, 2));
    }
    private bool IsInRange(Vector3 position)
    {
        return true;
    }
}