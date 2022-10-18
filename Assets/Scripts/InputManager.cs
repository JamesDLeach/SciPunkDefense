using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<Touch> touchList;
    private Vector3 currMousePos;
    private Vector3 prevMousePos;
    private Vector3 deltaMousePos;

    private void InitTouchList()
    {
        // Keep track of delta mouse pos
        currMousePos = Input.mousePosition;
        deltaMousePos = currMousePos - prevMousePos;
        prevMousePos = currMousePos;
        touchList = new List<Touch>(Input.touches);
        if (!SystemInfo.deviceType.Equals(DeviceType.Handheld) && Input.GetMouseButton(0))
        {
            Touch newTouch = new Touch();
            newTouch.position = currMousePos;
            newTouch.deltaPosition = deltaMousePos;
            newTouch.phase = TouchPhase.Moved;
            touchList.Add(newTouch);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitTouchList();
    }

    // Update is called once per frame
    void Update()
    {
        InitTouchList();
    }
}
