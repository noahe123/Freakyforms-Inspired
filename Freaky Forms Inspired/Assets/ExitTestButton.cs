using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class ExitTestButton : MonoBehaviour, IPointerUpHandler
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
        Invoke("ResetRigidbody", 0f);
        Invoke("ShowUI", .4f);
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.GetComponent<Image>().enabled = false;
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;

        FindObjectOfType<AudioManager>().Play("Shrink");

        //fix selection
    }


    public void ShowUI()
    {
        manager.transformList.transform.parent.gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(2).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(3).gameObject.SetActive(true);
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.GetComponent<Image>().enabled = true;
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        manager.transformList.transform.parent.parent.GetChild(6).gameObject.SetActive(false);

    }

    public void ResetRigidbody()
    {
        foreach (BodyPartChildDetect body in FindObjectsOfType<BodyPartChildDetect>())
        {

            Rigidbody2D rb = body.transform.GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.constraints = RigidbodyConstraints2D.None;

            rb.transform.position = body.transform.parent.GetComponent<BodyPart>().oldPos;
            rb.transform.rotation = body.transform.parent.GetComponent<BodyPart>().oldRot;

            rb.GetComponent<BodyPartChildDetect>().Invoke("EnableWheelCollision", 0f);

        }
    }

}
