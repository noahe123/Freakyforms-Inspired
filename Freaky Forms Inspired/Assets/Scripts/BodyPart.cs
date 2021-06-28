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
    private SpriteRenderer sprRenderer;
    private bool soldierSelected;
    public static bool dragSelectedSoldiersAllowed, mouseOverSoldier;
    private Vector2 mousePos;
    private float dragOffsetX, dragOffsetY;



    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        soldierSelected = false;
        dragSelectedSoldiersAllowed = false;
        mouseOverSoldier = false;


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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BodyPartSelectionManager>())
        {
            //sprRenderer.color = new Color(1f, 0f, 0f, 1f);
            //enable outline pulse
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            soldierSelected = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BodyPartSelectionManager>() && Input.GetMouseButton(0))
        {
            //sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            //disable outline pulse
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            soldierSelected = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBodyPart();
            if (overTrash == true)
            {

                Destroy(gameObject);
            }
        }

        if (!selected)
        {
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        //selection code***********************

        if (Input.GetMouseButtonDown(0))
        {
            dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }

        // And ofcourse I need to get mouse position

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            //sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            //disable outline pulse
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }


    private void OnMouseDown()
    {
        mouseOverSoldier = true;
    }

    private void OnMouseUp()
    {
        mouseOverSoldier = false;
        dragSelectedSoldiersAllowed = false;
    }

    private void OnMouseDrag()
    {
        dragSelectedSoldiersAllowed = true;

        if (!soldierSelected)
        {
            dragSelectedSoldiersAllowed = false;
        }


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
    }


    private void FixedUpdate()
    {
        if (!releasedBodyPart)
        {
            MoveBodyPart();
        }
    }

    public void SelectState(bool selectState)
    {
        selected = selectState;
    }

    public void ReleaseBodyPart()
    {
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(-1);
        //enable outline pulse
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);

        releasedBodyPart = true;
    }

    public void GrabBodyPart()
    {
        SelectState(true);
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(0);

        //disable outline pulse
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

        releasedBodyPart = false;
    }

    void MoveBodyPart()
    {
        Vector3 vector = new Vector3(Input.mousePosition.x-Screen.width*.45f, Input.mousePosition.y-Screen.height*.43f, Camera.main.nearClipPlane);
        //Debug.Log(Camera.main.ScreenToWorldPoint(vector));
        transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(vector*10) , followSpeed);
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
