using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartChildDetect : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.parent.parent.GetComponent<BodyPart>().GrabBodyPart();
    }
}
