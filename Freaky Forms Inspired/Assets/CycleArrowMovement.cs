using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleArrowMovement : MonoBehaviour
{

    public List<Transform> oldTargets;
    public List<Transform> targets;
    public MultipleTargetCamera cam;

    public Vector3 offset;
    public Vector3 magOffset;
    public float smoothTime = 0.5f;

    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter = 50;

    private Vector3 velocity;

    public float maxZoomMagnification;

    Transform origin;

    bool cycleDir;

    private void Start()
    {
        origin = GameObject.Find("Origin").transform;

        cam = Camera.main.GetComponent<MultipleTargetCamera>();
        targets = cam.targets;
        oldTargets = targets;

        cycleDir = GetComponent<TransformationButton>().cycleLeft;
    }

    private void LateUpdate()
    {
        if (targets != oldTargets)
        {
            targets = cam.targets;
            oldTargets = targets;
        }
        Move();
    }
    /*
    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime * 6);
    }*/

    private void Move()
    {
        float greatestDist = 0 ;
        if (targets.Count > 1)
        {
             greatestDist = GetGreatestDistance();
        }
        Vector3 newOffset = Vector3.zero;
        if (!cam.magnify)
        {
            if (cycleDir)
            {
                newOffset = (offset + 80 * new Vector3(-Mathf.Sqrt(greatestDist / 8), 0, 0)) * Screen.width / 800;
            }
            else
            {
                newOffset = (offset + 80 * new Vector3(Mathf.Sqrt(greatestDist / 8), 0, 0)) * Screen.width / 800;
            }
        }
        else if (cam.magnify)
        {
            if (cycleDir)
            {
                newOffset = (offset + magOffset + 80 * new Vector3(-Mathf.Sqrt(greatestDist / 8), 0, 0)) * Screen.width / 800;
            }
            else
            {
                newOffset = (offset + magOffset + 80 * new Vector3(Mathf.Sqrt(greatestDist / 8), 0, 0)) * Screen.width / 800;
            }
        }
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint+newOffset;
        //get world position
        Vector2 myPositionOnScreen = Camera.main.ViewportToWorldPoint(gameObject.transform.position);
        Vector3 finalPosition = new Vector3(myPositionOnScreen.x*.001f, myPositionOnScreen.y * -.001f, 0f) + newOffset;

        transform.position = Vector3.SmoothDamp(finalPosition, newPosition, ref velocity, smoothTime);
    }

    private float GetGreatestDistance()
    {

        var bounds = new Bounds(targets[0].position, Vector3.zero);

       targets.Remove(origin);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        targets.Add(origin);

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        targets.Remove(origin);

        int i = 0;
        while (i < targets.Count)
        {
            bounds.Encapsulate(targets[i].position);
            i++;
        }
       targets.Add(origin);

        return bounds.max;
    }
}
