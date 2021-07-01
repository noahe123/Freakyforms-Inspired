using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartChildDetect : MonoBehaviour
{
    BodyPart greatGrandParentBodyPart;

    //UI COlor Circle
    GameObject colorCircle;
    private void Start()
    {
        colorCircle = GameObject.Find("Color Circle");
        greatGrandParentBodyPart = transform.parent.parent.parent.GetComponent<BodyPart>();
    }
    private void OnMouseDown()
    {
        //greatGrandParentBodyPart.GrabBodyPart();


        //update num selected
        //FindObjectOfType<BodyPartSelectionManager>().numSelected += 1;

    }

    private void OnMouseOver()
    {
        if (colorCircle.GetComponent<Image>().enabled == true)
        {
            Vector3 lastMousePosition = Vector3.zero;
            if (Input.mousePosition != lastMousePosition)
            {
                lastMousePosition = Input.mousePosition;
                foreach (BodyPart body in FindObjectsOfType<BodyPart>())
                {
                    body.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
                }
                FindObjectOfType<BodyPartSelectionManager>().GetObjectOnTop().transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    
    private void OnMouseExit()
    {
        if (colorCircle.GetComponent<Image>().enabled == true)
        {
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //deactivate 
        if (Input.GetMouseButtonDown(0) && greatGrandParentBodyPart.releasedBodyPart == true)
        {
            // greatGrandParentBodyPart.SelectState(false);
            greatGrandParentBodyPart.selected = false;
        }
    }
}
