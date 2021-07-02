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
                newTargets.Add(targets[0]);
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
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
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
        if (magnify == true)
        {
            targets.Remove(origin);
        }
        while (i < targets.Count)
        {
            bounds.Encapsulate(targets[i].position);
            i++;
        }

        targets.Add(origin);

        return bounds.center;
    }
}