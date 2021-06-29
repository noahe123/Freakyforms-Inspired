using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ColorButton : MonoBehaviour, IPointerDownHandler
{

    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        Button btn = myButton.GetComponent<Button>();

    }

    public void OnPointerDown(PointerEventData data)
    {
        //GameObject.FindGameObjectWithTag("Manager").GetComponent<BodyPartSelectionManager>().selectedBodyPart.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color
           //= transform.GetChild(1).gameObject.GetComponent<Image>().color;
    }

}
