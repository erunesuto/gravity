using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{

    private Rigidbody rigidbody;
    protected Vector3 spawmPosition;
    private GameObject pickObjectDestination;
    public static float pickDistance = 3f;
    [Tooltip("True if use the general gravity. False if use its own gravity.")]
    public bool thisItemUseGravity = true;
    private float itemOwnGravityX;//save the value of the gravity for ownGravity items
    private float itemOwnGravityY;
    public bool itemFreezeRotation = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //pickObjectDestination = GameObject.Find("/PlayerRB/PickObject2");
        pickObjectDestination = GameObject.Find("/PlayerRB/Main Camera/PickObject");
        
        spawmPosition= new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
      

    }

   private void OnMouseDown()
    {
        if(Vector3.Distance(transform.position, pickObjectDestination.transform.position) < pickDistance)//YOu cannot pick up the object if you are too far away
        {
            GetComponent<Collider>().enabled = false;
            /*if (GetComponent<BoxCollider>())
            {
                GetComponent<BoxCollider>().enabled = false;
            }else if (GetComponent<SphereCollider>())
            {
                GetComponent<SphereCollider>().enabled = false;
            }*/

            rigidbody.freezeRotation = true;
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            rigidbody.useGravity = false;
            
            manageOwnGravityOnPickUp();
            
            this.transform.position = pickObjectDestination.transform.position;
            this.transform.parent = GameObject.Find("PickObject").transform;

        }     
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        rigidbody.constraints = RigidbodyConstraints.None;

        if (itemFreezeRotation == true)
        {
            rigidbody.freezeRotation = true;
        }
        else
        {
            rigidbody.freezeRotation = false;
        }

        if (thisItemUseGravity)
        {
            rigidbody.useGravity = true;
        }
        
        if (thisItemUseGravity == false)
        {
            this.GetComponent<ItemGravity>().gravityX = itemOwnGravityX;
            this.GetComponent<ItemGravity>().gravityY = itemOwnGravityY;
        }

        GetComponent<Collider>().enabled = true;
        /*if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().enabled = true;

        }else if (GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().enabled = true;
        }*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Respawmer"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position = spawmPosition;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawmer"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position = spawmPosition;
        }
    }

    void manageOwnGravityOnPickUp()
    {
        if(thisItemUseGravity == false)
        {
            itemOwnGravityX = this.GetComponent<ItemGravity>().gravityX;
            itemOwnGravityY = this.GetComponent<ItemGravity>().gravityY;

            this.GetComponent<ItemGravity>().gravityX = 0;
            this.GetComponent<ItemGravity>().gravityY = 0;
        }
    }


}

