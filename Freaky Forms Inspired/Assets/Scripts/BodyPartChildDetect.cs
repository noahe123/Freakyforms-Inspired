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

    //joint motor
    JointMotor2D jointMotor;
    JointSuspension2D jointSuspension;


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if ((GetComponent<FixedJoint2D>() != null && GetComponent<FixedJoint2D>().connectedBody != collision.rigidbody) || GetComponent<FixedJoint2D>() == null)
                {/*
                    testing = true;
                    if (collision.transform.GetComponent<FixedJoint2D>() != null)
                    {/*
                        foreach(FixedJoint2D collisionJoint in collision.transform)
                        {

                        }*/
                    /*
                        if (collision.transform.GetComponent<FixedJoint2D>().connectedBody != rb)
                        {
                            gameObject.AddComponent(typeof(FixedJoint2D));
                            gameObject.GetComponent<FixedJoint2D>().connectedBody = collision.rigidbody;
                        }
                    }
                    else if (collision.transform.GetComponent<FixedJoint2D>() == null)
                    {
                        gameObject.AddComponent(typeof(FixedJoint2D));
                        gameObject.GetComponent<FixedJoint2D>().connectedBody = collision.rigidbody;
                    }
                       /* //disable constraints
                        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;*/

                    if (transform.parent.GetComponent<BodyPart>().bodyPartType == "Body" || transform.parent.GetComponent<BodyPart>().bodyPartType == "Head" || transform.parent.GetComponent<BodyPart>().bodyPartType == "Mouth")
                    {
                        //****************** joint connector code!!! *********************
                        // creates joint
                        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                        // sets joint position to point of contact
                        joint.anchor = collision.GetContact(0).point;
                     
                        // conects the joint to the other object
                        joint.connectedBody = collision.transform.GetComponent<Rigidbody2D>();

                        //joint.autoConfigureDistance = false;
                        //joint.distance = 0;

                        // Stops objects from continuing to collide and creating more joints
                        joint.enableCollision = false;
                    }
                    else if (transform.parent.GetComponent<BodyPart>().bodyPartType == "Wheel")
                    {
                        float tempRotZ = collision.transform.localEulerAngles.z*Mathf.Deg2Rad;

                        //****************** wheel code!!! *********************
                        // creates joint
                        WheelJoint2D joint = gameObject.AddComponent<WheelJoint2D>();

                        joint.autoConfigureConnectedAnchor = false;
                        joint.anchor = Vector2.zero;

                        
                        joint.connectedAnchor = (-new Vector2(collision.transform.position.x * (1/ collision.transform.localScale.x), collision.transform.position.y * (1 / collision.transform.localScale.y)) 
                            + new Vector2((collision.GetContact(0).otherCollider.transform.position.x * (1/ collision.transform.localScale.x)), collision.GetContact(0).otherCollider.transform.position.y * (1 / collision.transform.localScale.y)))
                            * 2;

                        tempRotZ = -tempRotZ + Mathf.Atan2(joint.connectedAnchor.y , joint.connectedAnchor.x);

                        joint.connectedAnchor = new Vector2(Mathf.Cos(tempRotZ), Mathf.Sin(tempRotZ)) * joint.connectedAnchor.magnitude;

                        joint.connectedBody = collision.transform.GetComponent<Rigidbody2D>();
                     
                        // Stops objects from continuing to collide and creating more joints
                        joint.enableCollision = false;

                        // joint.autoConfigureDistance = false;
                        // joint.distance = 0;
                        joint.useMotor = true;
                        jointMotor.motorSpeed = 200;
                        jointMotor.maxMotorTorque = 1000;
                        jointSuspension.frequency = 20;
                        joint.motor = jointMotor;
                        joint.suspension = jointSuspension;
                    }

                }
            }
        }
    }


}
