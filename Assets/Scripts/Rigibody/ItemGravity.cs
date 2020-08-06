using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGravity : MonoBehaviour
{
    Rigidbody rigidbody;
    public float gravityX;
    public float gravityY;
    private Vector3 velocity;
    private GameObject pickObjectDestination;

    // Start is called before the first frame update
    void Start()
    {
        pickObjectDestination = GameObject.Find("/PlayerRB/Main Camera/PickObject");
        rigidbody = GetComponent<Rigidbody>();
    }

    /*private void Update()
    {
        rigidbody.AddForce(gravityX, gravityY, 0);
    }*/
    private void Update()
    {
        //transform.localEulerAngles = new Vector3(0, 0, 0);
        if (Input.GetButton("PickUp") && pickObjectDestination.transform.childCount <= 1 &&
            transform.parent == pickObjectDestination.transform &&
            Vector3.Distance(transform.position, pickObjectDestination.transform.position) < PickObject.pickDistance )
        {
            
            rigidbody.AddForce(0, 0, 0);
            transform.position = pickObjectDestination.transform.position;  
            
        }
        else
        {
            rigidbody.AddForce(gravityX, gravityY, 0);
            
        }
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        

        if (transform.position == pickObjectDestination.transform.position)
        {
            Debug.Log("misma posicion");
            velocity = new Vector3(1, 1, 1);
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            Debug.Log("distinta posicion");
            velocity = new Vector3(gravityX, gravityY, 0);
            transform.position += velocity * Time.deltaTime;

        }
        //transform.position += velocity * Time.deltaTime;
        Debug.Log(transform.position +" " + velocity);
        
    }*/


}
