using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController2 : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 15;
    private float xAxis, zAxis;

    [Header("Ground things")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    [Header("Gravity things")]
    public float gravity = -9.81f;
    public float rotationTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        movement();

        jump();

        changeGravity();

        aki
        //crear una funcion que mantenga los grados fijos 0, 90 ,180 -90 (?)
      //ajustar cuando se puede cambiar de gravedad, isgorunded y tal
    }

    void movement()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xAxis, 0, zAxis);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddRelativeForce(0, jumpForce, 0);
        }
    }

    void changeGravity()//al girar no gira del todo, añadir un pequeño salto.
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) /*&& isGrounded*/ )//cambiar el key por button
        {
            Physics.gravity = new Vector3(0, gravity, 0);//gravity goes up
            StartCoroutine(RotateFixedDegrees(Vector3.back * 180, rotationTime));
            StartCoroutine(RepositionZAngle(180, rotationTime + 0.01f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) /*&& isGrounded /*&& Time.timeScale == 1*/)
        {
            Physics.gravity = new Vector3(0, -gravity, 0);//gravity goes down
            StartCoroutine(RotateFixedDegrees(Vector3.back * 0, rotationTime));
            StartCoroutine(RepositionZAngle(0, rotationTime + 0.01f));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Physics.gravity = new Vector3(-gravity, 0, 0);//gravity goes left
            StartCoroutine(RotateFixedDegrees(Vector3.back * 90, rotationTime));
            StartCoroutine(RepositionZAngle(-90, rotationTime + 0.01f));

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) /*&& Time.timeScale == 1*/)
        {  
            Physics.gravity = new Vector3(gravity, -0, 0);//gravity goes right
            StartCoroutine(RotateFixedDegrees(Vector3.back * -90, rotationTime));
            StartCoroutine(RepositionZAngle(90, rotationTime + 0.01f));

        }

        test();
    }

    void repositionZAngle()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
    }
    void test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
    }

    void restartTimeScale()//does not used
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

    #region COROUTINES
    //COROUTINES
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

    IEnumerator RotateFixedDegrees(Vector3 finalRotation, float inTime)//rotate to Xposition in X seconds TEST
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
  
    }

    IEnumerator RepositionZAngle(int degrees, float delayTime)
    {
        
        yield return new WaitForSeconds(delayTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, degrees);
    }
    #endregion
}
