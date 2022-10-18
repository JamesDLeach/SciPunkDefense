using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Built following this guide:
 * https://www.youtube.com/watch?v=KkYco_7-ULA
 * Credit to user https://github.com/ditzel
 */
class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public bool isRotationAllowed;
    private Plane plane;
    private List<Touch> touchList;

    private Vector3 PlanePositionDelta(Touch touch)
    {
        // do nothing if touch hasn't moved
        if (!touch.phase.Equals(TouchPhase.Moved))
        {
            return Vector3.zero;
        }

        // calculate delta
        Ray prevRay = mainCamera.ScreenPointToRay(touch.position - touch.deltaPosition);
        Ray currRay = mainCamera.ScreenPointToRay(touch.position);
        if (plane.Raycast(prevRay, out var prevEnter) && plane.Raycast(currRay, out var currEnter))
        {
            return prevRay.GetPoint(prevEnter) - currRay.GetPoint(currEnter);
        }

        return Vector3.zero;
    }

    private Vector3 PlanePosition(Vector2 screenPos)
    {
        // get a ray from the screen
        Ray currRay = mainCamera.ScreenPointToRay(screenPos);
        // if the ray intersects the plane, return the intersection
        if (plane.Raycast(currRay, out var currEnter))
        {
            return currRay.GetPoint(currEnter);
        }

        return Vector3.zero;
    }

    private void HandleScroll()
    {
        plane.SetNormalAndPosition(transform.up, transform.position); // Update Plane
        if (touchList[0].phase.Equals(TouchPhase.Moved))
        {
            mainCamera.transform.Translate(PlanePositionDelta(touchList[0]), Space.World);
        }
    }

    private void HandleZoom()
    {
        Vector3 pos1 = PlanePosition(touchList[0].position);
        Vector3 pos2 = PlanePosition(touchList[1].position);
        Vector3 pos1b = PlanePosition(touchList[0].position - touchList[0].deltaPosition);
        Vector3 pos2b = PlanePosition(touchList[1].position - touchList[1].deltaPosition);

        float zoom = Vector3.Distance(pos1, pos2) /
                   Vector3.Distance(pos1b, pos2b);

        if (zoom == 0 || zoom > 10)
        {
            return;
        }

        // Move camera by the ray
        mainCamera.transform.position = Vector3.LerpUnclamped(pos1, mainCamera.transform.position, 1 / zoom);

        if (isRotationAllowed && pos2b != pos2)
        {
            mainCamera.transform.RotateAround(pos1, plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, plane.normal));
        }
    }

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        // Init touchList
        touchList = GameManager.InputManager.touchList;
        if (touchList.Count >= 1)
        {
            HandleScroll();
        }
        if (touchList.Count >= 2)
        {
            HandleZoom();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}