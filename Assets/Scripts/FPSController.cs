
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class FPSController : MonoBehaviour

{

    CharacterController characterController;

    [Header("Opciones de personaje")]
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Vector3 gravityV3 = new Vector3(0, 20f, 0);
    public Vector3 gravityV3Inverted = new Vector3(0, -20f, 0);

    [Header("Opciones de camara")]
    public Camera cam;
    public float mouseHorizontal = 3.0f;
    public float mouseVertical = 2.0f;
    public float minRotation = -90.0f;
    public float maxRotation = 90.0f;
    float inputHorizontalMouse, inputVerticalMouse;


    private Vector3 move = Vector3.zero;

    void Start()

    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }



    void Update()

    {
        //cambio de gravedad
        /*if (Input.GetKeyDown(KeyCode.G))//cambiar el key por button
        {
            Debug.Log("Pulsado G");
            //gravity = -gravity;
            gravity = Mathf.Lerp(gravity, -gravity, 2000f );
            //gravityV3 = Vector3.Lerp(gravityV3, -gravityV3, 2000);
        }*/

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ChangeGravity(gravity, -gravity, 5f));
        }
       

        //Movimiento de camara
        inputHorizontalMouse = mouseHorizontal * Input.GetAxis("Mouse X");
        inputVerticalMouse += mouseVertical * Input.GetAxis("Mouse Y");

        inputVerticalMouse = Mathf.Clamp(inputVerticalMouse, minRotation, maxRotation);
        cam.transform.localEulerAngles = new Vector3(-inputVerticalMouse, 0, 0);
        transform.Rotate(0, inputHorizontalMouse, 0);

        //Movimiento del personaje
        if (characterController.isGrounded)

        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift))//cambiar el key por button
            {
                move = transform.TransformDirection(move) * runSpeed;
            }
            else
            {
                move = transform.TransformDirection(move) * walkSpeed;
            }


            if (Input.GetKey(KeyCode.Space))//cambiar el key por button
            {
                move.y = jumpSpeed;
            }
        }

        move.y -= gravity * Time.deltaTime;
        //move.y -= gravityV3.y * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);

    }

    //Corrutina
    IEnumerator ChangeGravity(float gravityStart, float gravityEnd, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            gravity = Mathf.Lerp(gravityStart, gravityEnd, elapsed / duration);
            //transform.Rotate(0,0, Mathf.Lerp(0, -90, duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        gravity = gravityEnd;
    }

}



