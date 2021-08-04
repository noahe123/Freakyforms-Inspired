using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class TestButton : MonoBehaviour, IPointerUpHandler
{

    BodyPartSelectionManager manager;
    void Start()
    {
        manager = FindObjectOfType<BodyPartSelectionManager>().GetComponent<BodyPartSelectionManager>();
        Button btn = GetComponent<Button>();
    }

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerUp(PointerEventData eventData)
    {
        manager.transformList.SetActive(false);
        foreach (BodyPart body in FindObjectsOfType<BodyPart>())
        {
            if (body.transform.GetChild(0).childCount > 1)
            {
                Rigidbody2D rb = body.transform.GetChild(0).GetChild(1).GetComponent<Rigidbody2D>();

                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.gravityScale = 4;

                //get position
                body.oldPos = rb.transform.position;
                body.oldRot = rb.transform.rotation;

                Invoke("DisableConstraints", .1f);

                FindObjectOfType<AudioManager>().Play("Grow");

            }

        }
        HideUI();
    }



    public void DisableConstraints()
    {
        foreach (BodyPartChildDetect body in FindObjectsOfType<BodyPartChildDetect>())
        {

                Rigidbody2D rb = body.transform.GetComponent<Rigidbody2D>();

                rb.constraints = RigidbodyConstraints2D.None;

        }
    }

    public void HideUI()
    {
        manager.transformList.transform.parent.gameObject.SetActive(false);
        manager.transformList.transform.parent.parent.GetChild(0).gameObject.SetActive(false);
        manager.transformList.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
        manager.transformList.transform.parent.parent.GetChild(2).gameObject.SetActive(false);
        manager.transformList.transform.parent.parent.GetChild(3).gameObject.SetActive(false);
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.SetActive(true);

    }

}
