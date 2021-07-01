using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ButtonBodyPartUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool moveLeft;
    public Vector3 offset = new Vector3(30f, -30f, 0);


    public void OnPointerDown(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = false;
        transform.position += offset;

        if (moveLeft)
        {
            transform.parent.GetChild(0).gameObject.GetComponent<BodyPartUI>().MoveUILeft();
        }
        if (!moveLeft)
        {
            transform.parent.GetChild(0).gameObject.GetComponent<BodyPartUI>().MoveUIRight();
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = true;
        transform.position -= offset;

    }
}