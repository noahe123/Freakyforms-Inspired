using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public float followSpeed = 20;
    public bool releasedBodyPart = false;
    public Vector2 followOffset;
    PolygonCollider2D polygonCollider2D;
    Sprite sprite;
    GameObject objectWithSprite;
    public bool overTrash = false;
    public bool selected = false;

    //selection variables
    private Vector2 mousePos;
    public float dragOffsetX, dragOffsetY;



    private void Start()
    {

        SelectState(true);
        objectWithSprite = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        polygonCollider2D = objectWithSprite.GetComponent<PolygonCollider2D>();
        sprite = objectWithSprite.GetComponent<SpriteRenderer>().sprite;

        if (!Input.GetMouseButton(0))
        {
            ReleaseBodyPart();
        }
        else
        {
            followOffset = new Vector3(0, 0, -Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).z);
        }

        //update collider
        UpdatePolygonCollider2D();

    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragOffsetX = 0;
            dragOffsetY = 0;
            if (selected == true)
            {
                ReleaseBodyPart();

            }

            if (overTrash == true)
            {

                Destroy(gameObject);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)));
                Debug.Log(transform.position);

            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            dragOffsetX = mousePos.x - transform.position.x;
            dragOffsetY = mousePos.y - transform.position.y;

           
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

        }


    }



    private void FixedUpdate()
    {
        if (!releasedBodyPart && selected)
        {
            MoveBodyPart();
        }
    }

    public void SelectState(bool selectState)
    {

        selected = selectState;

        if (selectState == false)
        {
            //disable outline pulse
           transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

        }
        else
        {
            //enable outline pulse
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);

        }

    }

    public void ReleaseBodyPart()
    {

        dragOffsetX = 0;
        dragOffsetY = 0;
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(-1);
        //enable outline pulse
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        releasedBodyPart = true;
        SelectState(true);
    }

    public void GrabBodyPart()
    {
        //disable outline pulse
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

        SelectState(true);
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(0);
        releasedBodyPart = false;
    }

    
    public void MoveBodyPart()
    {
/*
        Vector3 vector = new Vector3(Input.mousePosition.x-Screen.width*.45f, Input.mousePosition.y-Screen.height*.43f, Camera.main.nearClipPlane);
        Debug.Log(Camera.main.ScreenToWorldPoint(vector));
       transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(vector*10) + new Vector3(dragOffsetX, dragOffsetY, 0), followSpeed);*/

        
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.position = Vector2.Lerp(transform.position, new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY), followSpeed);
    }

    // Store these outside the method so it can reuse the Lists (free performance)
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    public void UpdatePolygonCollider2D(float tolerance = 0.05f)
    {
        polygonCollider2D.pathCount = sprite.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider2D.pathCount; i++)
        {
            sprite.GetPhysicsShape(i, points);
            LineUtility.Simplify(points, tolerance, simplifiedPoints);
            polygonCollider2D.SetPath(i, simplifiedPoints);
        }
    }
}
