using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Rendering;



public class BodyPartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 spawnOffset;
    private Sprite mySprite;
    private Button myButton;
    public Transform prefabBody;
    public Transform prefabWheel;

    public Vector3 offset = new Vector3(4f, -4f, 0);
    GameObject manager;
    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
        transform.Translate(new Vector3(0, 0, -transform.position.z));
        mySprite = transform.GetChild(3).GetComponent<Image>().sprite;
        myButton = GetComponent<Button>();
        Button btn = myButton.GetComponent<Button>();

        //set text
        if (mySprite.name.Contains("head"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "head";
        }
        else if (mySprite.name.Contains("body"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "body";
        }
        else if (mySprite.name.Contains("mouth"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "mouth";
        }
        else if (mySprite.name.Contains("wheel"))
        {
            transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "wheel";
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = false;
        transform.position += offset;
        if (manager.GetComponent<BodyPartSelectionManager>().numParts < manager.GetComponent<BodyPartSelectionManager>().maxParts)
        {
            InstantiateBodyPart();
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        GetComponent<Shadow>().enabled = true;
        transform.position -= offset;
    }

    void InstantiateBodyPart()
    {
        //**************** spawn head / body
        if (mySprite.name.Contains("body") || mySprite.name.Contains("head") || mySprite.name.Contains("mouth"))
        {
            InstantiateBody();
        }
        else if (mySprite.name.Contains("wheel"))
        {
            InstantiateWheel();
        }
    }

    void InstantiateBody()
    {
        prefabBody.GetChild(0).GetChild(0).GetComponent<SpriteMask>().sprite = mySprite;
        prefabBody.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mySprite;
        prefabBody.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = mySprite;

        Vector3 vector = new Vector3(Input.mousePosition.x - Screen.width * .45f, Input.mousePosition.y - Screen.height * .43f, Camera.main.nearClipPlane);
        manager.GetComponent<BodyPartSelectionManager>().numParts++;
        manager.GetComponent<BodyPartSelectionManager>().DisplayParts();

        Transform spawn = Instantiate(prefabBody, Camera.main.ScreenToWorldPoint(vector * 10) + spawnOffset, Quaternion.identity);
        FindObjectOfType<MultipleTargetCamera>().GetComponent<MultipleTargetCamera>().targets.Add(spawn.transform.GetChild(0));
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(0);

        //make child of player
        spawn.transform.parent = player.transform;

        //sorting layer
        spawn.gameObject.GetComponent<SortingGroup>().sortingOrder = manager.GetComponent<BodyPartSelectionManager>().numParts * 50 - manager.GetComponent<BodyPartSelectionManager>().numParts;

        spawn.GetComponent<BodyPart>().GrabBodyPart();
    }

    void InstantiateWheel()
    {
        prefabBody.GetChild(0).GetChild(0).GetComponent<SpriteMask>().sprite = mySprite;
        prefabBody.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mySprite;
        prefabBody.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = mySprite;

        Vector3 vector = new Vector3(Input.mousePosition.x - Screen.width * .45f, Input.mousePosition.y - Screen.height * .43f, Camera.main.nearClipPlane);
        manager.GetComponent<BodyPartSelectionManager>().numParts++;
        manager.GetComponent<BodyPartSelectionManager>().DisplayParts();

        Transform spawn = Instantiate(prefabWheel, Camera.main.ScreenToWorldPoint(vector * 10) + spawnOffset, Quaternion.identity);
        FindObjectOfType<MultipleTargetCamera>().GetComponent<MultipleTargetCamera>().targets.Add(spawn.transform.GetChild(0));
        FindObjectOfType<TrashBin>().GetComponent<TrashBin>().SetTrashState(0);

        //make child of player
        spawn.transform.parent = player.transform;

        //sorting layer
        spawn.gameObject.GetComponent<SortingGroup>().sortingOrder = manager.GetComponent<BodyPartSelectionManager>().numParts * 50 - manager.GetComponent<BodyPartSelectionManager>().numParts;

        spawn.GetComponent<BodyPart>().GrabBodyPart();
    }
}
