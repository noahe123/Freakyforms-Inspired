using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;
using UnityEngine.Rendering;

public class TransformationButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 offset = new Vector3(4f, -4f, 0);

    [SerializeField]
    private bool clone, flip, stretchX, squashX, stretchY, squashY, expand, shrink, rotateCW, rotateCCW, layerUp, layerDown, cycleLeft, cycleRight;

    [SerializeField]
    GameObject[] selectedBodies = new GameObject[50];

    public Vector3 cloneOffset;

    [SerializeField]
    float scaleFactor, outlineScaleFactor;

    public float rotAmount;

    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        manager =  GameObject.FindGameObjectWithTag("Manager");
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


    public void OnPointerExit(PointerEventData eventData)
    {
        for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
        {
            selectedBodies[currBody] = null;
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
            int i = 0;
            foreach (GameObject body in GameObject.FindGameObjectsWithTag("Body Outline"))
            {
                selectedBodies[i] = body;
                i++;
            }

            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == null || manager.GetComponent<BodyPartSelectionManager>().numParts >= manager.GetComponent<BodyPartSelectionManager>().maxParts)
                {
                    return;
                }

                selectedBodies[currBody].transform.parent.parent.parent.GetComponent<BodyPart>().SelectState(false);

                //count
                manager.GetComponent<BodyPartSelectionManager>().numParts++;
                manager.GetComponent<BodyPartSelectionManager>().DisplayParts();


                GameObject newObj = Instantiate(selectedBodies[currBody].transform.parent.parent.parent.gameObject, selectedBodies[currBody].transform.parent.parent.parent.position, Quaternion.identity);
                newObj.transform.Translate(cloneOffset * (newObj.transform.localScale.x / 2));

                //newObj.GetComponent<BodyPart>().SelectState(false);

                FindObjectOfType<MultipleTargetCamera>().GetComponent<MultipleTargetCamera>().targets.Add(newObj.transform);

                newObj.GetComponent<SortingGroup>().sortingOrder = manager.GetComponent<BodyPartSelectionManager>().numParts * 50 - manager.GetComponent<BodyPartSelectionManager>().numParts;

            }
        }
        else if (flip)
        {

            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }

                if (selectedBodies[currBody].transform.parent.parent.transform.rotation.y == 0)
                {

                    selectedBodies[currBody].transform.parent.parent.transform.Rotate(new Vector3(0, -180, 0));
                }
                else
                {

                    selectedBodies[currBody].transform.parent.parent.transform.Rotate(new Vector3(0, 180, 0));

                }
            }
        }
        else if (stretchX)
        {

            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                bool stretchCondition = (((selectedBodies[currBody].transform.parent.localRotation.z * 180 < -45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 >= -135) || (selectedBodies[currBody].transform.parent.localRotation.z * 180 > 45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 <= 135)));

                if (!stretchCondition)
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * scaleFactor,
                            selectedBodies[currBody].transform.parent.transform.localScale.y,
                            selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.parent.transform.localScale =
                          new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (-outlineScaleFactor + (scaleFactor)),
                          selectedBodies[currBody].transform.parent.transform.localScale.y,
                          selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
                else
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                      new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                      selectedBodies[currBody].transform.parent.transform.localScale.y * scaleFactor,
                      selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.localScale.x,
                            selectedBodies[currBody].transform.localScale.y * ((scaleFactor - outlineScaleFactor)),
                            selectedBodies[currBody].transform.localScale.z);
                }

            }
        }
        else if (squashX)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }

                bool stretchCondition = (((selectedBodies[currBody].transform.parent.localRotation.z * 180 < -45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 >= -135) || (selectedBodies[currBody].transform.parent.localRotation.z * 180 > 45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 <= 135)));

                // Debug.Log(selectedBodies[currBody].transform.parent.rotation.z);
                if (!stretchCondition)
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (1 / scaleFactor),
                        selectedBodies[currBody].transform.parent.transform.localScale.y,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.parent.transform.localScale =
                           new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (1 / (-outlineScaleFactor + scaleFactor)),
                           selectedBodies[currBody].transform.parent.transform.localScale.y,
                           selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
                else
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                      new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                      selectedBodies[currBody].transform.parent.transform.localScale.y * (1 / scaleFactor),
                      selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.localScale.x,
                            selectedBodies[currBody].transform.localScale.y * (1 / (scaleFactor - outlineScaleFactor)),
                            selectedBodies[currBody].transform.localScale.z);
                }
            }
        }
        else if (stretchY)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                bool stretchCondition = (((selectedBodies[currBody].transform.parent.localRotation.z * 180 < -45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 >= -135) || (selectedBodies[currBody].transform.parent.localRotation.z * 180 > 45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 <= 135)));

                if (stretchCondition)
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * scaleFactor,
                            selectedBodies[currBody].transform.parent.transform.localScale.y,
                            selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.parent.transform.localScale =
                          new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (-outlineScaleFactor + (scaleFactor)),
                          selectedBodies[currBody].transform.parent.transform.localScale.y,
                          selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
                else
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                      new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                      selectedBodies[currBody].transform.parent.transform.localScale.y * scaleFactor,
                      selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.localScale.x,
                            selectedBodies[currBody].transform.localScale.y * ((scaleFactor - outlineScaleFactor)),
                            selectedBodies[currBody].transform.localScale.z);
                }
            }
        }
        else if (squashY)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                bool stretchCondition = (((selectedBodies[currBody].transform.parent.localRotation.z * 180 < -45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 >= -135) || (selectedBodies[currBody].transform.parent.localRotation.z * 180 > 45 && selectedBodies[currBody].transform.parent.localRotation.z * 180 <= 135)));

                if (stretchCondition)
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                        new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (1 / scaleFactor),
                        selectedBodies[currBody].transform.parent.transform.localScale.y,
                        selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.parent.transform.localScale =
                           new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x * (1 / (-outlineScaleFactor + scaleFactor)),
                           selectedBodies[currBody].transform.parent.transform.localScale.y,
                           selectedBodies[currBody].transform.parent.transform.localScale.z);
                }
                else
                {
                    selectedBodies[currBody].transform.parent.transform.localScale =
                      new Vector3(selectedBodies[currBody].transform.parent.transform.localScale.x,
                      selectedBodies[currBody].transform.parent.transform.localScale.y * (1 / scaleFactor),
                      selectedBodies[currBody].transform.parent.transform.localScale.z);

                    selectedBodies[currBody].transform.localScale =
                            new Vector3(selectedBodies[currBody].transform.localScale.x,
                            selectedBodies[currBody].transform.localScale.y * (1 / (scaleFactor - outlineScaleFactor)),
                            selectedBodies[currBody].transform.localScale.z);
                }
            }
        }
        else if (expand)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.transform.localScale *= scaleFactor;

                selectedBodies[currBody].transform.localScale *= (scaleFactor - outlineScaleFactor);
            }
        }
        else if (shrink)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.transform.localScale *= 1 / scaleFactor;

                selectedBodies[currBody].transform.localScale *= 1 / (scaleFactor - outlineScaleFactor);
            }
        }
        else if (rotateCCW)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.transform.Rotate(new Vector3(0, 0, -rotAmount * Mathf.Sign(selectedBodies[currBody].transform.parent.parent.transform.rotation.y)));
            }
        }
        else if (rotateCW)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.transform.Rotate(new Vector3(0, 0, rotAmount * Mathf.Sign(selectedBodies[currBody].transform.parent.parent.transform.rotation.y)));
            }
        }
        else if (layerUp)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.parent.parent.gameObject.GetComponent<SortingGroup>().sortingOrder += 50;
            }
        }
        else if (layerDown)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Shrink");
                }
                if (selectedBodies[currBody] == null)
                {

                    return;
                }
                selectedBodies[currBody].transform.parent.parent.parent.gameObject.GetComponent<SortingGroup>().sortingOrder -= 50;
            }
        }
        else if (cycleLeft)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {
                    return;
                }
                CycleSpriteRight(selectedBodies[currBody].transform.parent.parent.parent.GetComponent<BodyPart>().bodyPartType,
                    selectedBodies[currBody].transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite, 
                    selectedBodies[currBody].transform.parent.parent.parent.gameObject);
            }
        }
        else if (cycleRight)
        {
            for (int currBody = 0; currBody < selectedBodies.Length; currBody++)
            {
                if (selectedBodies[currBody] == selectedBodies[0])
                {
                    FindObjectOfType<AudioManager>().Play("Grow");
                }
                if (selectedBodies[currBody] == null)
                {
                    return;
                }
                CycleSpriteRight(selectedBodies[currBody].transform.parent.parent.parent.GetComponent<BodyPart>().bodyPartType,
                    selectedBodies[currBody].transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite,
                    selectedBodies[currBody].transform.parent.parent.parent.gameObject);
            }
        }

    }

    public void CycleSpriteRight(string bodyType, Sprite sprite, GameObject myObj)
    {
        string[] guids2 = AssetDatabase.FindAssets("" + bodyType + " t:Sprite", new[] { "Assets/Resources/Images/Body Parts"});

        int myIndex = 0;
        int count = 0 ;

        
        foreach (string guid2 in guids2)
        {
            string guid2Name = AssetDatabase.GUIDToAssetPath(guid2);
            if (guid2Name.Contains(sprite.name))
            {
                myIndex = count;
            }
            count++;
        }
        string spriteToChange = "";
        if (cycleRight)
        {
            if (myIndex + 1 <= guids2.Length-1)
            {
                spriteToChange = AssetDatabase.GUIDToAssetPath(guids2[myIndex + 1]);
            }
            else
            {
                spriteToChange = AssetDatabase.GUIDToAssetPath(guids2[0]);
            }
        }
        else
        {
            if (myIndex - 1 >= 0)
            {
                spriteToChange = AssetDatabase.GUIDToAssetPath(guids2[myIndex - 1]);
            }
            else
            {
                spriteToChange = AssetDatabase.GUIDToAssetPath(guids2[guids2.Length-1]);
            }

        }

        spriteToChange = spriteToChange.Replace(".png", "");
        spriteToChange = spriteToChange.Replace("Assets/Resources/", "");
        Sprite mySprite = Resources.Load<Sprite>(spriteToChange);
        //change sprite
        myObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mySprite;
        myObj.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = mySprite;
        myObj.transform.GetChild(0).GetChild(0).GetComponent<SpriteMask>().sprite = mySprite;
        myObj.GetComponent<BodyPart>().UpdatePolygonCollider2D();
        myObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<DropShadow>().ResetShadow();
    }
}
