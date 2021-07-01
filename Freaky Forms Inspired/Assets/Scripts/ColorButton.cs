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
    public Vector3 offset = new Vector3(2.4f,-2.4f,0);

    bool isPressed;

    GameObject objectOutlining;

	void Start()
	{
        myColor = transform.GetChild(1).GetComponent<Image>().color;
        colorCircle = transform.parent.parent.parent.GetChild(4).gameObject;

        Button btn = GetComponent<Button>();
	}

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("Paint Select");

        transform.GetChild(0).GetComponent<Shadow>().enabled = false;
        transform.position += offset;
        isPressed = true;
        colorCircle.GetComponent<Image>().color = myColor;
        colorCircle.GetComponent<Image>().enabled = true;
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position -= offset;

        transform.GetChild(0).GetComponent<Shadow>().enabled = true;

        colorCircle.GetComponent<Image>().enabled = false;
        isPressed = false;
        colorCircle.GetComponent<Image>().enabled = false;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, 1500f);

        foreach (var hit in hits)
        {
            if (hit.collider.name.Contains("Body Template Normals"))
            {
                if (hit.collider.gameObject.transform.parent.GetChild(1).gameObject.activeInHierarchy)
                {
                 
                        FindObjectOfType<AudioManager>().Play("Paint");

                        hit.collider.gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().color = myColor;
                    
                }

            }
        }
    }
}
