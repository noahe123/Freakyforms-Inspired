using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSelectionManager : MonoBehaviour
{
    public GameObject selectedBodyPart;

    public GameObject transformList;

    public int numParts;

    private void Start()
    {
        transformList = GameObject.Find("TransformList");
        transformList.SetActive(false);
    }

    private void OnMouseUp()
    {
        Debug.Log(numParts);

    }
    /*
// Start is called before the first frame update
private void Update()
{
    if(Input.GetMouseButtonDown(0))
    {
        ClickSelect();
    }
}

GameObject ClickSelect()
{
    //Converting $$anonymous$$ouse Pos to 2D (vector2) World Pos
    Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

    if (hit)
    {
        //Debug.Log(hit.transform.name);
        return hit.transform.gameObject;
    }
    else return null;
}
    */
}
