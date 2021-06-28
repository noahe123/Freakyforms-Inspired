using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class BodyPartUI : MonoBehaviour
{
    public float incrementValue;
    public void MoveUILeft()
    {
        if ((transform.GetChild(0).GetComponent<RectTransform>().localPosition.x + incrementValue*-5) > (transform.GetChild(0).childCount * -incrementValue))
        {
            transform.GetChild(0).GetComponent<RectTransform>().localPosition += new Vector3(-incrementValue, 0, 0);
        }
    }
    public void MoveUIRight()
    {
        if (transform.GetChild(0).GetComponent<RectTransform>().localPosition.x < -(incrementValue*5 + 1))  
        {
            transform.GetChild(0).GetComponent<RectTransform>().localPosition += new Vector3(incrementValue, 0, 0);
        }
    }
}
