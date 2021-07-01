using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    public GameObject GetObjectOnTop()
    {
        //Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        int maxLayer = 0;
        GameObject selected = null;
        foreach (RaycastHit2D hit in hits)
        {
            int myLayer = hit.transform.parent.parent.parent.GetComponent<SortingGroup>().sortingOrder;
            if (myLayer > maxLayer)
            {
                maxLayer = myLayer;
                selected = hit.transform.parent.parent.parent.gameObject;
            }
            //Debug.Log(hit.transform.name);
        }
        return selected;
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
