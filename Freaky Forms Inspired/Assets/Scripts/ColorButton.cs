using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ColorButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public Button myButton;
    public GameObject colorCircle;
    public Color myColor;

    bool isPressed;

    GameObject objectOutlining;

	void Start()
	{
        myColor = transform.GetChild(1).GetComponent<Image>().color;
        colorCircle = transform.parent.parent.parent.GetChild(4).gameObject;

        Button btn = GetComponent<Button>();
	}

    private void Update()
    {

        if (isPressed == true)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, 1500f);


                foreach (var hit in hits)
                {
                    if (hit.collider.name.Contains("Body Template Normals"))
                    {
                        objectOutlining = hit.collider.gameObject;
                        objectOutlining.transform.parent.GetChild(1).gameObject.SetActive(true);
                    }   
                    else
                    {
                        if (objectOutlining != null)
                        {
                            objectOutlining.transform.parent.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }

 

        }

    }

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        colorCircle.GetComponent<Image>().color = myColor;
        colorCircle.GetComponent<Image>().enabled = true;
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {

        isPressed = false;
        colorCircle.GetComponent<Image>().enabled = false;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, 1500f);

            foreach (var hit in hits)
            {
                if (hit.collider.name.Contains("Body Template Normals"))
                {
                     hit.collider.gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().color = myColor;

            }
        }
    }
}
