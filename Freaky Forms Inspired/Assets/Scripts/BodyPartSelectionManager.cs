using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class BodyPartSelectionManager : MonoBehaviour
{
    public GameObject selectedBodyPart;

    public GameObject transformList;
    public GameObject magnifyingGlass;
    //public GameObject colorsList;


    public int numParts;

    public int maxParts;

    public int remainingParts;

    public GameObject partsCounter;


    private void Start()
    {


        magnifyingGlass = GameObject.Find("Magnifying Glass");
        partsCounter = GameObject.Find("Parts Counter");
        transformList = GameObject.Find("TransformList");
        //colorsList = GameObject.Find("ColorsList");

        transformList.SetActive(false);
        magnifyingGlass.SetActive(false);

        DisplayParts();


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
 
            {
                int myLayer = hit.transform.parent.parent.GetComponent<SortingGroup>().sortingOrder;
                if (myLayer > maxLayer)
                {
                    maxLayer = myLayer;
                    selected = hit.transform.parent.parent.gameObject;
                }
            }
            //Debug.Log(hit.transform.name);
        }
        return selected;
    }

    public void DisplayParts()
    {
        remainingParts = maxParts - numParts;
        if (remainingParts <= 0)
        {
            partsCounter.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.black;
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "left.";

        }
        else if (remainingParts <= 3)
        {
            partsCounter.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.red;
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.red;
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "left!";

        }
        else
        {
            partsCounter.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(0/255, ((float)140 / (float)255), 255/255, 1);
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = new Color(0 / 255, ((float)140 / (float)255), 255 / 255, 1);
            partsCounter.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "left.";

        }
        partsCounter.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + (maxParts - numParts);
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
