using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartChildDetect : MonoBehaviour
{
    BodyPart greatGrandParentBodyPart;

    //UI COlor Circle
    GameObject colorCircle;

    Rigidbody2D rb;

    public bool testing = false;
    public bool constraintsFlag = true;

    //joint stuff
     JointMotor2D jointMotor;
    JointSuspension2D jointSuspension;

    public Vector2 oldPos = Vector2.zero;

    public WheelJoint2D wheelJoint;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

     
        colorCircle = GameObject.Find("Color Circle");
        greatGrandParentBodyPart = transform.parent.parent.GetComponent<BodyPart>();
    }
    private void OnMouseDown()
    {
        //greatGrandParentBodyPart.GrabBodyPart();


        //update num selected
        //FindObjectOfType<BodyPartSelectionManager>().numSelected += 1;

    }

    private void OnMouseOver()
    {
        //IF DYNAMIC
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            return;
        }

        if (colorCircle.GetComponent<Image>().enabled == true)
        {
            Vector3 lastMousePosition = Vector3.zero;
            if (Input.mousePosition != lastMousePosition)
            {
                lastMousePosition = Input.mousePosition;
                foreach (BodyPart body in FindObjectsOfType<BodyPart>())
                {
                    if (body.transform.GetChild(0).GetChild(0).childCount > 1)
                    {
                         body.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
                    }
                }
                
                FindObjectOfType<BodyPartSelectionManager>().GetObjectOnTop().transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    
    private void OnMouseExit()
    {
        //IF DYNAMIC
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            return;
        }

        if (colorCircle.GetComponent<Image>().enabled == true)
        {
            transform.parent.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //IF DYNAMIC
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            return;
        }

        //deactivate 
        if (Input.GetMouseButtonDown(0) && greatGrandParentBodyPart.releasedBodyPart == true)
        {
            // greatGrandParentBodyPart.SelectState(false);
            greatGrandParentBodyPart.selected = false;
        }


    }

    /* private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer == 9 && (Vector2)transform.position != oldPos)
        {
            oldPos = transform.position;
            Debug.Log("Colliding with layer");
            ResetWheel(collision);
        }
}*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
       //GetComponent<BodyPartChildDetect>().wheelJoint.enableCollision = true;

    }
    private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 9)
            {

                if ((GetComponent<FixedJoint2D>() != null && GetComponent<FixedJoint2D>().connectedBody != collision.rigidbody) || GetComponent<FixedJoint2D>() == null)
                {
                        if (transform.parent.GetComponent<BodyPart>().bodyPartType == "Body" || transform.parent.GetComponent<BodyPart>().bodyPartType == "Head" || transform.parent.GetComponent<BodyPart>().bodyPartType == "Mouth")
                        {
                            //****************** joint connector code!!! *********************
                            // creates joint
                            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                            // sets joint position to point of contact
                            joint.anchor = collision.GetContact(0).point;
                            // conects the joint to the other object
                            joint.connectedBody = collision.transform.GetComponent<Rigidbody2D>();


                            // Stops objects from continuing to collide and creating more joints
                            joint.enableCollision = false;
                        }
   

                }
            //****************** wheel code!!! *********************
            if (transform.parent.GetComponent<BodyPart>().bodyPartType == "Wheel" && (Vector2)transform.position != oldPos)
            {
                ResetWheel(collision);
                oldPos = transform.position;

            }
        }
        
    }

     void ResetWheel(Collision2D collision)
    {

        Debug.Log("Resetting Wheel");

        float tempRotZ = collision.transform.localEulerAngles.z * Mathf.Deg2Rad;



            if (GetComponent<WheelJoint2D>() != null)
            {
                // gets joint
                wheelJoint = GetComponent<WheelJoint2D>();
            }
            else
            {
                // creates joint
                wheelJoint = gameObject.AddComponent<WheelJoint2D>();
            }

            wheelJoint.autoConfigureConnectedAnchor = false;
            wheelJoint.anchor = Vector2.zero;


            wheelJoint.connectedAnchor = (-new Vector2(collision.transform.position.x * (1 / collision.transform.localScale.x), collision.transform.position.y * (1 / collision.transform.localScale.y))
                + new Vector2((collision.GetContact(0).otherCollider.transform.position.x * (1 / collision.transform.localScale.x)), collision.GetContact(0).otherCollider.transform.position.y * (1 / collision.transform.localScale.y)))
                * 2;

            tempRotZ = -tempRotZ + Mathf.Atan2(wheelJoint.connectedAnchor.y, wheelJoint.connectedAnchor.x);

        wheelJoint.connectedAnchor = new Vector2(Mathf.Cos(tempRotZ), Mathf.Sin(tempRotZ)) * wheelJoint.connectedAnchor.magnitude;

        wheelJoint.connectedBody = collision.transform.GetComponent<Rigidbody2D>();

        // Stops objects from continuing to collide and creating more joints

        // joint.autoConfigureDistance = false;
        // joint.distance = 0;
         wheelJoint.useMotor = true;
            jointMotor.motorSpeed = 200;
            jointMotor.maxMotorTorque = 1000;
            jointSuspension.frequency = 20;
            wheelJoint.motor = jointMotor;
            wheelJoint.suspension = jointSuspension;

;    }

    public void DisableWheelCollision()
    {
        if (GetComponent<WheelJoint2D>() != null)
        {
            GetComponent<BodyPartChildDetect>().wheelJoint.enableCollision = false;
        }
    }
    public void EnableWheelCollision()
    {
        if (GetComponent<WheelJoint2D>() != null)
        {
            GetComponent<BodyPartChildDetect>().wheelJoint.enableCollision = true;
        }
    }
}

