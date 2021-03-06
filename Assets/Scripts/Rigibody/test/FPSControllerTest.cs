﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerTest : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 5;
    private float xAxis, zAxis;

    [Header("Ground things")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask scenarioMask;
    bool isGrounded;

    [Header("Gravity things")]
    public float gravity = -9.81f;
    [Tooltip("Time the player is rotating")]
    public float rotationTime = 1f;
    private int gravityID = 2; //controles what "gravity position" player is(1-up, 2d-own, 3-left o 4-right) Used to avoid change to the smae gravity twice
    private bool changeGravityButtonDown = false;//flag. Controlls if any change gravity button is pushed
    [HideInInspector]
    public bool changeGravityAvailable = true;//It is activated when step some floors

    public GameObject invertChangeGravityControlls;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        //invertChangeGravityControlls = GravityPositionController.invertChangeGravityControlls;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(invertChangeGravityControlls);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, scenarioMask);
        jump();

        changeGravity();

    }

    private void FixedUpdate()
    {
        movement();

    }

    void movement()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xAxis, 0, zAxis);
        transform.Translate(movement * speed * Time.deltaTime);
        //rigidbody.AddRelativeForce(movement * speed * Time.deltaTime);

    }

    void jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddRelativeForce(0, jumpForce, 0);
        }
    }

    void changeGravity()
    {
        if (canChangeGravity())
        {
            /////
            //if (Input.GetKeyDown(KeyCode.Q))
            if (Input.GetButtonDown("GravityLeft"))
            {
                if (GravityPositionController.invertChangeGravityControlls == false)//manage where player looking to make change gravity more friendly. Q always change gravity to the left no matter where you are facing
                {
                    gravityID--;
                    changeGravityButtonDown = true;
                }
                else if (GravityPositionController.invertChangeGravityControlls == true)
                {
                    gravityID++;
                    changeGravityButtonDown = true;
                }
                
            }
            
            else if (Input.GetButtonDown("GravityRight"))
            {
                if (GravityPositionController.invertChangeGravityControlls == false)
                {
                    gravityID++;
                    changeGravityButtonDown = true;
                }
                else
                {
                    gravityID--;
                    changeGravityButtonDown = true;
                }      
            }
            else if (Input.GetButtonDown("GravityUp"))
            {
                gravityID += 2;
                changeGravityButtonDown = true;
            }
            ////
            if (gravityID == 0)
            {
                gravityID = 4;
            }
            else if (gravityID == 5)
            {
                gravityID = 1;
            }
            else if (gravityID == 6)
            {
                gravityID = 2;
            }
            //////
            if (changeGravityButtonDown == true)//if press Q, 2 or E
            {
                if (gravityID == 4) /*&& isGrounded*/ //cambiar el key por button
                                                      //if (Input.GetButtonDown("GravityUp") && gravityID != 1)
                {
                    Physics.gravity = new Vector3(0, gravity, 0);//gravity goes up
                    //StartCoroutine(RotateFixedDegrees(Vector3.back * 180, rotationTime));
                    StartCoroutine(RotateFixedDegrees(new Vector3(0, transform.localEulerAngles.y, 180)/*Vector3.back * 180*/, rotationTime));
                    StartCoroutine(RepositionZAngle(180, rotationTime + 0.01f));
                    //gravityID = 4;//"flag"
                    changeGravityButtonDown = false;
                }
                else if (gravityID == 2) /*&& gravityID != 2*/
                {
                    Physics.gravity = new Vector3(0, -gravity, 0);//gravity goes down
                    StartCoroutine(RotateFixedDegrees(new Vector3(0, transform.localEulerAngles.y, 0), rotationTime));
                    StartCoroutine(RepositionZAngle(0, rotationTime + 0.01f));
                    //gravityID = 2;//"flag"
                    changeGravityButtonDown = false;
                }
                else if (gravityID == 1)
                {
                    Physics.gravity = new Vector3(-gravity, 0, 0);//gravity goes left
                    StartCoroutine(RotateFixedDegrees(new Vector3(0, transform.localEulerAngles.y, -90), rotationTime));
                    StartCoroutine(RepositionZAngle(-90, rotationTime + 0.01f));
                    //gravityID = 1;//"flag"
                    changeGravityButtonDown = false;
                }
                else if (gravityID == 3)
                {
                    Physics.gravity = new Vector3(gravity, -0, 0);//gravity goes right
                    StartCoroutine(RotateFixedDegrees(new Vector3(0, transform.localEulerAngles.y, 90), rotationTime));
                    StartCoroutine(RepositionZAngle(90, rotationTime + 0.01f));
                    //gravityID = 3;//"flag"
                    changeGravityButtonDown = false;
                }
            }

        }
    }
    bool canChangeGravity()
    {
        //if (isGrounded && (transform.localEulerAngles.z == 90 || transform.localEulerAngles.z == -90 || transform.localEulerAngles.z == 180 || transform.localEulerAngles.z == 0))
        // "elbueno" if (isGrounded && (transform.localEulerAngles.z == 90 || transform.localEulerAngles.z == 270 || transform.localEulerAngles.z == 180  || transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 360))
        if (isGrounded && changeGravityAvailable && (Mathf.Round(transform.localEulerAngles.z) == 90f ||
            Mathf.Round(transform.localEulerAngles.z) == 270 || Mathf.Round(transform.localEulerAngles.z) == 180 ||
            Mathf.Round(transform.localEulerAngles.z) == 0 || Mathf.Round(transform.localEulerAngles.z) == 360))

        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*void repositionZAngles()
    {
        if (gravityID == 1)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -90);
        }
        else if (gravityID == 2)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
        else if (gravityID == 3)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
        }
        else if (gravityID == 4)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180);
        }
    }*/




    #region COROUTINES
    //COROUTINES
    IEnumerator RotateMe(Vector3 byAngles, float inTime)//rotate X degrees in X seconds. NOT USED
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator RotateFixedDegrees(Vector3 finalRotation, float inTime)//rotate to Xrotation in X seconds. Used
    {

        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }

    }

    /*
     IEnumerator RotateFixedDegrees(Vector3 finalRotation, float inTime)//rotate to Xrotation in X seconds. Used
    {
        
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
  
    }
     * */

    //change Z degrees rotation after X seconds
    //Used to fixed the rotation after change gravity. Some how sometimes does not rotate 90º but 89.81º 
    IEnumerator RepositionZAngle(int degrees, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, degrees);
    }
    #endregion
}
