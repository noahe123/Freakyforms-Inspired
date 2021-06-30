using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 initialMousePosition, currentMousePosition;
    private BoxCollider2D boxColl;

    //box selection
    float i;
    public float rate = 1.5f;
    public Color colourStart, colourEnd;


    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
        lineRend.widthMultiplier = .25f;
    }

    // Update is called once per frame
    void Update()
    {
        // When left mouse button is pressed and mouse pointer is not over any soldier
        // I create four points at mouse position

        if (Input.GetMouseButtonDown(0) && !Soldier.mouseOverSoldier && !NonSelectableZone.mouseOverZone && !ButtonZone.mouseOverZone)
        {
            lineRend.positionCount = 4;
            //initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //new line
            initialMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            //******
            lineRend.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRend.SetPosition(1, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRend.SetPosition(2, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRend.SetPosition(3, new Vector2(initialMousePosition.x, initialMousePosition.y));

            // This BoxSelection game object gets a box collider which is set as a trigger
            // Center of this collider is at BoxSelection position

            boxColl = gameObject.AddComponent<BoxCollider2D>();
            boxColl.isTrigger = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0) && !Soldier.mouseOverSoldier  && !NonSelectableZone.mouseOverZone && !ButtonZone.mouseOverZone)
        {
            foreach (Soldier body in FindObjectsOfType<Soldier>())
            {
                //body.SelectState(false);
                body.soldierSelected = false;
                body.transform.parent.parent.parent.GetComponent<BodyPart>().SelectState(false);
            }
        }

        // While mouse button is being held down I can draw a rectangle
        // Those four points get corresponding coordinates depending on
        // mouse initial position when button was pressed for the first time
        // and its current position

        if (Input.GetMouseButton(0) && !Soldier.mouseOverSoldier && !NonSelectableZone.mouseOverZone && !ButtonZone.mouseOverZone)
        {


            //currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //new line*****
            currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            //******
            lineRend.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRend.SetPosition(1, new Vector2(initialMousePosition.x, currentMousePosition.y));
            lineRend.SetPosition(2, new Vector2(currentMousePosition.x, currentMousePosition.y));
            lineRend.SetPosition(3, new Vector2(currentMousePosition.x, initialMousePosition.y));

            // BoxSelection gameobjects position is at the middle of the box drawn

            transform.position = (currentMousePosition + initialMousePosition) / 2;

            // Box collider boundaries outline that box drawn

            boxColl.size = new Vector2(
            Mathf.Abs(initialMousePosition.x - currentMousePosition.x),
            Mathf.Abs(initialMousePosition.y - currentMousePosition.y));
        }

        // When mouse button is released box is wiped, collider is destroyed
        // and BoxSelection gameobject goes back to the center of the scene

        if (Input.GetMouseButtonUp(0))
        { 
            lineRend.positionCount = 0;
            Destroy(boxColl);
            transform.position = Vector3.zero;
        }



    }

    private void FixedUpdate()
    {
        //line renderer
        // Blend towards the current target colour
        i += Time.deltaTime * rate;
        // Animate the Shininess value
        Color myColor = Color.Lerp(colourStart, colourEnd, Mathf.PingPong(i * 2, 1));

        // If we've got to the current target colour, choose a new one
        if (i >= 1)
        {
            i = 0;
        }


        lineRend.startColor = myColor;
        lineRend.endColor = myColor;
    }
}
