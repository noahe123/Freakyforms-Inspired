using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ExitTestButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    BodyPartSelectionManager manager;
    void Start()
    {
        manager = FindObjectOfType<BodyPartSelectionManager>().GetComponent<BodyPartSelectionManager>();
        Button btn = GetComponent<Button>();
    }

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    //Do this when the mouse click on this selectable UI object is released.
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


                Invoke("DisableConstraints", .1f);
                HideUI();

            }

        }
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

    public void ShowUI()
    {
        manager.transformList.transform.parent.gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(2).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(3).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.SetActive(false);

    }
}
