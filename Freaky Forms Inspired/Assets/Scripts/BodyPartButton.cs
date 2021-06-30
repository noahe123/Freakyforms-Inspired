using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class BodyPartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 spawnOffset;
    private Sprite mySprite;
    private Button myButton;
    public Transform prefab;
    public Vector3 offset = new Vector3(4f, -4f, 0);
    GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        transform.Translate(new Vector3(0, 0, -transform.position.z));
        mySprite = transform.GetChild(3).GetComponent<Image>().sprite;
        myButton = GetComponent<Button>();
        Button btn = myButton.GetComponent<Button>();

        //set text
        if (mySprite.name.Contains("head"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Head";
        }
        else if (mySprite.name.Contains("body"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Body";
        }
        else if (mySprite.name.Contains("mouth"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Mouth";
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = false;
        transform.position += offset;

        InstantiateBodyPart();
    }

    public void OnPointerUp(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = true;
        transform.position -= offset;
    }

    void InstantiateBodyPart()
    {
        prefab.GetChild(0).GetChild(0).GetComponent<SpriteMask>().sprite = mySprite;
        prefab.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mySprite;
        prefab.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = mySprite;
        Vector3 vector = new Vector3(Input.mousePosition.x - Screen.width * .45f, Input.mousePosition.y - Screen.height * .43f, Camera.main.nearClipPlane);
        Transform spawn = Instantiate(prefab, Camera.main.ScreenToWorldPoint(vector * 10) + spawnOffset, Quaternion.identity);
        manager.GetComponent<BodyPartSelectionManager>().numParts++;

        spawn.GetComponent<BodyPart>().GrabBodyPart();
    }

}
