using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ButtonBodyPartUI : MonoBehaviour, IPointerDownHandler
{
    public bool moveLeft;

    public void OnPointerDown(PointerEventData data)
    {
        if (moveLeft)
        {
            transform.parent.GetChild(0).gameObject.GetComponent<BodyPartUI>().MoveUILeft();
        }
        if (!moveLeft)
        {
            transform.parent.GetChild(0).gameObject.GetComponent<BodyPartUI>().MoveUIRight();
        }
    }

}
