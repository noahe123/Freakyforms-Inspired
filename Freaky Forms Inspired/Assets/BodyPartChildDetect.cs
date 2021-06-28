using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartChildDetect : MonoBehaviour
{
    BodyPart greatGrandParentBodyPart;
    private void Start()
    {
        greatGrandParentBodyPart = transform.parent.parent.parent.GetComponent<BodyPart>();
    }
    private void OnMouseDown()
    {
        greatGrandParentBodyPart.GrabBodyPart();
        greatGrandParentBodyPart.selected = true;
    }

    private void Update()
    {
        //deactivate 
        if (Input.GetMouseButtonDown(0) && greatGrandParentBodyPart.releasedBodyPart == true)
        {
            greatGrandParentBodyPart.SelectState(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            greatGrandParentBodyPart.overTrash = true;
            FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            greatGrandParentBodyPart.overTrash = false;

            FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(0);
        }
    }

}
