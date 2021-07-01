using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    //private SpriteRenderer sprRenderer;

    public bool soldierSelected;

    public static bool dragSelectedSoldiersAllowed, mouseOverSoldier;

    private Vector2 mousePos;

    public float dragOffsetX, dragOffsetY;

    public bool noOffsetPlease ;

    //my variables
    BodyPart greatGrandParentBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        //sprRenderer = GetComponent<SpriteRenderer>();
        soldierSelected = false;
        dragSelectedSoldiersAllowed = false;
        mouseOverSoldier = false;

        //my code
        greatGrandParentBodyPart = transform.parent.parent.parent.GetComponent<BodyPart>();


    }

    // When BoxSelections collider meets a soldier, soldier changes its color tint to Red
    // and soldier is marked as selected now

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>())
        {
            //enable outline pulse
            //transform.parent.GetChild(1).gameObject.SetActive(true);
            //greatGrandParentBodyPart.GrabBodyPart();
            greatGrandParentBodyPart.SelectState(true);
            soldierSelected = true;

            
        }
    }

    // When BoxSelection collider stops touching a soldier while left mouse button is still
    // being held down then soldeir gets its normal color tint and marked as not selected

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>() && Input.GetMouseButton(0))
        {
            //disable outline pulse
            //transform.parent.GetChild(1).gameObject.SetActive(false);
            greatGrandParentBodyPart.ReleaseBodyPart();
            soldierSelected = false;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            noOffsetPlease = false;

            if (soldierSelected == true)
            {
                transform.parent.GetChild(1).gameObject.SetActive(true);
            }
        }

        // When left mouse button is clicked I need to get an offset between mouse position
        // and a soldier. This offset will help me to drag soldier/soldiers from
        // its/their initial positions depending on mouse position whitout any "jumping" issues

        if (Input.GetMouseButtonDown(0))
        {
            /*
            dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            */



        }

        // And ofcourse I need to get mouse position
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        }

        //Pretty obvious piece of code I guess :-) If it's not just let me know.


        if (soldierSelected && dragSelectedSoldiersAllowed)
        {
            // transform.parent.parent.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
            //7-1
            transform.parent.parent.parent.GetComponent<BodyPart>().MoveBodyPart();

        }

        // If right mouse button is pressed then selection is reset
        /*
        if (Input.GetMouseButtonDown(1))
        {
            soldierSelected = false;
            dragSelectedSoldiersAllowed = false;
            //disable outline pulse
            //transform.parent.GetChild(1).gameObject.SetActive(false);
            //greatGrandParentBodyPart.ReleaseBodyPart();
        }*/
    }

    // No need to explain I hope

    private void OnMouseDown()
    {
        mouseOverSoldier = true;
        //7-1
        //greatGrandParentBodyPart.GrabBodyPart();
        FindObjectOfType<BodyPartSelectionManager>().GetObjectOnTop().GetComponent<BodyPart>().GrabBodyPart();
    }

    // Same here

    private void OnMouseUp()
    {


        //soldierSelected = false;

        mouseOverSoldier = false;
        dragSelectedSoldiersAllowed = false;
    }

    // Hope it's clear as well

    private void OnMouseDrag()
    {

        dragSelectedSoldiersAllowed = true;

        if (!soldierSelected)
        {
            dragSelectedSoldiersAllowed = false;
        }

        /*mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.parent.parent.transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);*/
        //greatGrandParentBodyPart.GrabBodyPart();
        //greatGrandParentBodyPart.selected = true;
    }
}
