using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class MagnifyingGlass : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Sprite spriteEnabled, spriteDisabled;
    void Start()
    {
        
    }

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Camera.main.gameObject.GetComponent<MultipleTargetCamera>().magnify == false)
        {
            GetComponent<Image>().sprite = spriteEnabled;
            Camera.main.gameObject.GetComponent<MultipleTargetCamera>().magnify = true;
        }
        else
        {
            GetComponent<Image>().sprite = spriteDisabled;

            Camera.main.gameObject.GetComponent<MultipleTargetCamera>().magnify = false;
        }
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
