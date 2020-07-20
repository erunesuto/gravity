using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController2 : MonoBehaviour
{

    public Camera camera;
    public float horizontalSpeed= 3;
    public float verticalSpeed = 3;

    float horizontalMouseMovement;
    float verticalMouseMovement;


    public float speed;

    private float xAxis, zAxis;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMouseMovement = horizontalSpeed * Input.GetAxis("Mouse X");
        verticalMouseMovement = verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalMouseMovement, 0);

        //verticalMouseMovement = Mathf.Clamp(verticalMouseMovement, -90, 90);
        //camera.transform.Rotate(-verticalMouseMovement, 0, 0);
        camera.transform.localRotation = Quaternion.Euler(-verticalMouseMovement, 0, 0);
        
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        /*if(camera.transform.eulerAngles.x > -90)
        {
            camera.transform.eulerAngles = new Vector3 (-90,0,0);
        }*/


        //movimiento
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xAxis, 0, zAxis);
        transform.Translate(movement * speed * Time.deltaTime);


    }
}
