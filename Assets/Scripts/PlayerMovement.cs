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


        /*if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }*/


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

        //gravity
        if (Input.GetKeyDown(KeyCode.G))//cambiar el key por button
        {
            Invoke("restartGravity", 0.1f);
            if (gravityInverse == true)
            {
                velocity.y = -(Mathf.Sqrt(jumpHeight * -2f * gravity * -1));//Player jumps
                gravity = -gravity;
                Time.timeScale = 0.5f;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                gravityInverse = false;

            }
            else
            {

                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);//Player jumps
                gravity = -gravity;
                Time.timeScale = 0.5f;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180);

                gravityInverse = true;

            }
            
        }
        aki
            //pulir la corutina y ponerla en la g
        //rotar el personaje y despues vamo viendo. Hacerlo con un corrutina
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(RotateMe(Vector3.back * 180, 1));
        }

            Debug.Log(gravity +" "+ Time.timeScale);
        
    }
    void restartGravity()
    {
        if (isGrounded)
        {
            Debug.Log("invocado");
            Time.timeScale = 1.0f;
        }
        else
        {
            Invoke("restartGravity", 0.1f);
        }
    }

    //coroutines
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}