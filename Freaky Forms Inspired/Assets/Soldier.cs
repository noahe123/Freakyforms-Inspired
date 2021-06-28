using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private SpriteRenderer sprRenderer;

    private bool soldierSelected;

    public static bool dragSelectedSoldiersAllowed, mouseOverSoldier;

    private Vector2 mousePos;

    private float dragOffsetX, dragOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        soldierSelected = false;
        dragSelectedSoldiersAllowed = false;
        mouseOverSoldier = false;
    }

    // When BoxSelections collider meets a soldier, soldier changes its color tint to Red
    // and soldier is marked as selected now

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>())
        {
            sprRenderer.color = new Color(1f, 0f, 0f, 1f);
            soldierSelected = true;
        }
    }

    // When BoxSelection collider stops touching a soldier while left mouse button is still
    // being held down then soldeir gets its normal color tint and marked as not selected

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>() && Input.GetMouseButton(0))
        {
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            soldierSelected = false;
        }
    }

    private void Update()
    {
        // When left mouse button is clicked I need to get an offset between mouse position
        // and a soldier. This offset will help me to drag soldier/soldiers from
        // its/their initial positions depending on mouse position whitout any "jumping" issues

        if (Input.GetMouseButtonDown(0))
        {
            /*
            dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            */
            dragOffsetX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)).y - transform.position.y;
        }

        // And ofcourse I need to get mouse position

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        }

        //Pretty obvious piece of code I guess :-) If it's not just let me know.

        if (soldierSelected && dragSelectedSoldiersAllowed)
        {
            transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
        }

        // If right mouse button is pressed then selection is reset

        if (Input.GetMouseButtonDown(1))
        {
            soldierSelected = false;
            dragSelectedSoldiersAllowed = false;
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    // No need to explain I hope

    private void OnMouseDown()
    {
        mouseOverSoldier = true;
    }

    // Same here

    private void OnMouseUp()
    {
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

        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
    }
}
