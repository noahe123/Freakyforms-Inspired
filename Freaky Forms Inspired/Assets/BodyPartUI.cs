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
        if ((transform.GetChild(0).GetComponent<RectTransform>().position.x + incrementValue*-12) > (transform.GetChild(0).childCount * -incrementValue))
        {
            transform.GetChild(0).GetComponent<RectTransform>().position += new Vector3(-incrementValue * (Screen.height / 350), 0, 0);
        }
    }
    public void MoveUIRight()
    {
        if (transform.GetChild(0).GetComponent<RectTransform>().position.x < -(incrementValue*-2 + 1))
        {
            transform.GetChild(0).GetComponent<RectTransform>().position += new Vector3(incrementValue * (Screen.height / 350), 0, 0);
        }
    }
}
