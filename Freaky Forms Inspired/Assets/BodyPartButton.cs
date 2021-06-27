using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class BodyPartButton : MonoBehaviour, IPointerDownHandler
{
    public Vector3 spawnOffset;
    private Sprite mySprite;
    private Button myButton;
    public Transform prefab;
    void Start()
    {

        transform.Translate(new Vector3(0, 0, -transform.position.z));
        mySprite = transform.GetChild(3).GetComponent<Image>().sprite;
        myButton = GetComponent<Button>();
        Button btn = myButton.GetComponent<Button>();

        //set text
        if (mySprite.name.Contains("Head"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "HEAD";
        }
        else if (mySprite.name.Contains("Body"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "BODY";
        }
        else if (mySprite.name.Contains("Mouth"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "MOUTH";
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        InstantiateBodyPart();
    }

    void InstantiateBodyPart()
    {
        prefab.GetChild(0).GetChild(0).GetComponent<SpriteMask>().sprite = mySprite;
        prefab.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mySprite;
        Vector3 vector = new Vector3(Input.mousePosition.x - Screen.width * .45f, Input.mousePosition.y - Screen.height * .43f, Camera.main.nearClipPlane);
        Instantiate(prefab, Camera.main.ScreenToWorldPoint(vector * 10) + spawnOffset, Quaternion.identity);
    }

}
