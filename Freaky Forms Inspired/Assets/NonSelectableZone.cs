using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonSelectableZone : MonoBehaviour
{
    //private SpriteRenderer sprRenderer;



    public static bool mouseOverZone;

    private Vector2 mousePos;


    // Start is called before the first frame update
    void Start()
    {

        mouseOverZone = false;

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        }
    }

    // No need to explain I hope

    private void OnMouseDown()
    {
        mouseOverZone = true;
    }

    // Same here

    private void OnMouseUp()
    {
        mouseOverZone = false;

    }

}
