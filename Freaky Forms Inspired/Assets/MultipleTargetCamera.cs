using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public Camera cam;
    public List<Transform> targets;

    // magnification
    public List<Transform> oldTargets, newTargets;
    public bool magnify;
    bool magnifyTargetSet = false;

    public Vector3 offset;
    public float smoothTime = 0.5f;

    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter = 50;

    private Vector3 velocity;

    float maxZoomInitial;
    public float maxZoomMagnification;

    Transform origin;

    public float outOfBoundsX, outOfBoundsY;

    public bool moveable = true;

    private void Start()
    {
        origin = GameObject.Find("Origin").transform;
        maxZoomInitial = maxZoom;
    }

    private void Reset()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        if (magnify && !magnifyTargetSet)
        {
            if (GameObject.FindGameObjectWithTag("Body Outline") != null)
            {
                FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("Grow");
                magnifyTargetSet = true;
                maxZoom = maxZoomMagnification;

                oldTargets.Clear();
                foreach(Transform target in targets)
                {
                    oldTargets.Add(target);
                }
                newTargets.Clear();

                foreach (GameObject magnifyTarget in GameObject.FindGameObjectsWithTag("Body Outline"))
                {
                    newTargets.Add(magnifyTarget.transform.parent.parent.parent);
                }
                //newTargets.Add(targets[0]);
                targets.Clear();
                foreach (Transform target in newTargets)
                {
                    targets.Add(target);
                }
            }
        }
        else if (!magnify && magnifyTargetSet)
        {
            maxZoom = maxZoomInitial;

            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("Shrink");
            magnifyTargetSet = false;


            if (oldTargets.Count > 0)
            {
                targets.Clear();

                foreach (Transform target in oldTargets)
                {
                    targets.Add(target);
                }
                newTargets.Clear();
                oldTargets.Clear();
            }
        }

            Move();
            Zoom();
        
    }

    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime*6);
    }

    private void Move()
    {
        Vector3 newPosition = transform.position;
        if (moveable)
        {
            var centerPoint = GetCenterPoint();
            newPosition = centerPoint + offset;
        }

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        // origin.transform.position = new Vector3(transform.position.x, transform.position.y, origin.transform.position.z);

        bool outOfBounds = false;
        if (transform.position.x > outOfBoundsX)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(outOfBoundsX, transform.position.y, transform.position.z), ref velocity, smoothTime/20);
            outOfBounds = true;
        }
        else if (transform.position.x < -outOfBoundsX)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(-outOfBoundsX, transform.position.y, transform.position.z), ref velocity, smoothTime/20);
            outOfBounds = true;

        }

        if (transform.position.y > outOfBoundsY)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, outOfBoundsY, transform.position.z), ref velocity, smoothTime/20);
            outOfBounds = true;

        }
        else if (transform.position.y < -10)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, -10, transform.position.z), ref velocity, smoothTime/20);
            outOfBounds = true;

        }
        if (outOfBounds)
        {
            origin.transform.position = new Vector3(newPosition.x, newPosition.y, origin.transform.position.z);
            outOfBounds = false;
        }



        //origin.transform.position = new Vector3(transform.position.x, transform.position.y, origin.transform.position.z);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        int i = 0;
        targets.Remove(origin);

        while (i < targets.Count)
        {
            bounds.Encapsulate(targets[i].position);
            i++;
        }

        targets.Add(origin);

        return bounds.center;
    }
}