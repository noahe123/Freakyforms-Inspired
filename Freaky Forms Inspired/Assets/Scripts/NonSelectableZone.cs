using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class NonSelectableZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //private SpriteRenderer sprRenderer;



    public static bool mouseOverZone;

    private Vector2 mousePos;


    // Start is called before the first frame update
    void Start()
    {

        mouseOverZone = false;

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        }
    }

    // No need to explain I hope

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseOverZone = true;
    }

    // Same here

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseOverZone = false;

    }

}
