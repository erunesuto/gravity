using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;



    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool gravityInverse = false;


    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        /*if (isGrounded && velocity.y < 0)//Dont know why, Brackeys recomendation
        {
            velocity.y = -2f;

        }else if (isGrounded && velocity.y > 0)
        {
            velocity.y = 2f;
        }*/

        movement();

        //changeGravity();
        changeGravity2();

        Debug.Log(Physics.gravity);
        
    }

    void movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButton("Jump") && isGrounded)
        {
            if (gravityInverse)
            {
                velocity.y = -(Mathf.Sqrt(jumpHeight * -2f * gravity * -1));//formula rara para que salte bocaabajo deberia funcionar
            }
            else
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }                 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void changeGravity2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) /*&& Time.timeScale == 1*/)//cambiar el key por button
        {
            Invoke("restartTimeScale", 0.1f);

            //velocity.y = -(Mathf.Sqrt(jumpHeight * -2f * gravity * -1));//Player jumps
            Physics.gravity = new Vector3(0, -gravity, 0);
            //Time.timeScale = 0.4f;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            //StartCoroutine(RotateMe(Vector3.back * 180, 0.5f));
            //gravityInverse = false;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) /*&& Time.timeScale == 1*/)
        {

            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);//Player jumps
            Physics.gravity = new Vector3(0, gravity, 0);
            //Time.timeScale = 0.4f;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180);
            //StartCoroutine(RotateMe(Vector3.back * 180, 0.5f));
            //gravityInverse = true;

        }

        
    }
    void changeGravity()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 1)//cambiar el key por button
        {
            Invoke("restartTimeScale", 0.1f);
            if (gravityInverse == true)
            {
                velocity.y = -(Mathf.Sqrt(jumpHeight * -2f * gravity * -1));//Player jumps
                gravity = -gravity;
                Time.timeScale = 0.4f;
                //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                StartCoroutine(RotateMe(Vector3.back * 180, 0.5f));
                gravityInverse = false;

            }
            else
            {

                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);//Player jumps
                gravity = -gravity;
                Time.timeScale = 0.4f;
                //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180);
                StartCoroutine(RotateMe(Vector3.back * 180, 0.5f));
                gravityInverse = true;

            }

        }
    }

    void restartTimeScale()
    {
        if (isGrounded)
        {
            //Debug.Log("invocado");
            Time.timeScale = 1.0f;
        }
        else
        {
            Invoke("restartTimeScale", 0.1f);
        }
    }

    //coroutines
    IEnumerator RotateMe(Vector3 byAngles, float inTime)//rotate X degrees in X seconds
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator RotateMe2(Vector3 finalRotation, float inTime)//rotate to Xposition in X seconds
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}