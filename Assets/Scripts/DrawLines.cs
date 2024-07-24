using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    public Camera cam = null;
    public LineRenderer lineRenderer = null;
    private Vector3 touchPos;
    private Vector3 pos;
    public Vector3 previousPos;
    public List<Vector3> linePositions = new List<Vector3>();
    public float minimumDistance = 0.05f;
    private float distance = 0;
    public float groundLevelY = 1f; // Set this to the Y-coordinate of your ground level

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrawing();
                    break;

                case TouchPhase.Moved:
                    ContinueDrawing();
                    break;

                case TouchPhase.Ended:
                    GameManager.Instance.player.MoveTowardsPath(linePositions, 0);
                    break;
            }
        }
    }

    private void StartDrawing()
    {
        linePositions.Clear();
        pos = GetTouchWorldPositionOnGround();
        previousPos = pos;
        linePositions.Add(pos);
    }

    private void ContinueDrawing()
    {
        pos = GetTouchWorldPositionOnGround();
        distance = Vector3.Distance(pos, previousPos);

        if (distance >= minimumDistance)
        {
            previousPos = pos;
            linePositions.Add(pos);
            lineRenderer.positionCount = linePositions.Count;
            lineRenderer.SetPositions(linePositions.ToArray());
        }
    }

    private Vector3 GetTouchWorldPositionOnGround()
    {
        Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, groundLevelY, 0));
        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
