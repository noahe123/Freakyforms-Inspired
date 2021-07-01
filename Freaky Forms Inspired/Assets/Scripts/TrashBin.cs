using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TrashBin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        SetTrashState(-1);
    }
    public void SetTrashState(int trashState)
    {
        if (trashState == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (trashState == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (trashState == -1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (BodyPart body in FindObjectsOfType<BodyPart>())
        {
            if (body.selected || body.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Soldier>().soldierSelected)
            {
                body.overTrash = true;
                SetTrashState(1);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (BodyPart body in FindObjectsOfType<BodyPart>())
        {
            if (body.selected || body.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Soldier>().soldierSelected)
            {
                body.overTrash = false;
                SetTrashState(0);
            }
        }
    }

}