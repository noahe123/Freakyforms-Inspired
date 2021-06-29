using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ColorCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Input.mousePosition;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Input.mousePosition, 20f*Time.deltaTime);

    }
}
