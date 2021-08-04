using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicsControl : MonoBehaviour
{
    bool turnedDynamic;
    BodyPartSelectionManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<BodyPartSelectionManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.GetChild(0).position = transform.GetChild(0).GetChild(1).position - transform.GetChild(0).GetChild(1).localPosition;
        if (!turnedDynamic && (transform.GetChild(0).GetChild(1).GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic))
        {
            turnedDynamic = true;

            manager.transformList.SetActive(false);
            transform.GetComponent<BodyPart>().enabled = false;
            transform.GetChild(0).GetChild(1).GetComponent<BodyPartChildDetect>().enabled = false;
            transform.GetChild(0).GetChild(1).GetComponent<Soldier>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

            transform.GetChild(0).GetChild(1).transform.parent = transform;
            transform.GetChild(0).parent = transform.GetChild(1);
        }
        else if (turnedDynamic && (transform.GetChild(0).GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic))
        {
            turnedDynamic = false;

            transform.GetChild(0).GetChild(0).transform.parent = transform;
            transform.GetChild(0).transform.parent = transform.GetChild(1);

            manager.transformList.SetActive(true);
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetComponent<BodyPart>().enabled = true;
            transform.GetChild(0).GetChild(1).GetComponent<BodyPartChildDetect>().enabled = true;
            transform.GetChild(0).GetChild(1).GetComponent<Soldier>().enabled = true;
        }
    }
}
