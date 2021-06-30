using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class TransformationButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler
{
    public Vector3 offset = new Vector3(4f, -4f, 0);

    [SerializeField]
    private bool clone, flip, stretchX, squashX, stretchY, squashY, expand, shrink, rotateCW, rotateCCW;

    [SerializeField]
    GameObject[] selectedBodies = new GameObject[50];

    public Vector3 cloneOffset;

    [SerializeField]
    float scaleFactor;

    public float rotAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int i = 0;
        foreach (GameObject body in GameObject.FindGameObjectsWithTag("Body Outline"))
        {
            selectedBodies[i] = body;
            i++;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Shadow>().enabled = false;
        transform.position += offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Shadow>().enabled = true;
        transform.position -= offset;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));


        //various transformations
        if (clone)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                    Instantiate(selectedBodies[currBody].transform.parent.parent.parent, selectedBodies[currBody].transform.parent.parent.parent.position += cloneOffset, Quaternion.identity);
                }
            }
            else if (flip)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * -1,
                        selectedBodies[currBody].transform.parent.transform.localScale.y,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
            }
            else if (stretchX)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * scaleFactor,
                        selectedBodies[currBody].transform.parent.transform.localScale.y,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
            }
            else if (squashX)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }

                selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (1 / scaleFactor),
                        selectedBodies[currBody].transform.parent.transform.localScale.y,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
            }
            else if (stretchY)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                        selectedBodies[currBody].transform.parent.transform.localScale.y * scaleFactor,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
            }
            else if (squashY)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                        selectedBodies[currBody].transform.parent.transform.localScale.y * (1 / scaleFactor),
                        selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
            }
            else if (expand)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                    selectedBodies[currBody].transform.parent.transform.localScale *= scaleFactor;
                }
            }
            else if (shrink)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                    selectedBodies[currBody].transform.parent.transform.localScale *= 1 / scaleFactor;
                }
            }
            else if (rotateCCW)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                    selectedBodies[currBody].transform.parent.transform.Rotate(new Vector3(0, 0, -rotAmount));
                }
             }
            else if (rotateCW)
            {
                for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
                {
                    if (selectedBodies[currBody] == null)
                    {
                        return;
                    }
                    selectedBodies[currBody].transform.parent.transform.Rotate(new Vector3(0, 0, rotAmount));
                }
            }

    }
}
