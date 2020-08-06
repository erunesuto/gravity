using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{

    private Rigidbody rigidbody;
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
    }

   /* private void Update()
    {
        if(Input.GetButton("PickUp"))
        {
            if (pickObjectDestination.transform.childCount < 1 && 
                Vector3.Distance(transform.position, pickObjectDestination.transform.position) < pickDistance)//YOu cannot pick up the object if you are too far away
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                //this.transform.position = pickObjectDestination.position;
                this.transform.position = pickObjectDestination.transform.position;
                this.transform.parent = GameObject.Find("PickObject").transform;
            }
        }
        else
        {
            this.transform.parent = null;
            if (thisItemUseGravity)
            {
                GetComponent<Rigidbody>().useGravity = true;
                
            }
            //GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BoxCollider>().enabled = true;
        }

        if( pickObjectDestination.transform.childCount > 0)
        {
            Debug.Log(pickObjectDestination.transform.childCount);
        }


    }*/

   private void OnMouseDown()
    {
        if(Vector3.Distance(transform.position, pickObjectDestination.transform.position) < pickDistance)//YOu cannot pick up the object if you are too far away
        {
            if (GetComponent<BoxCollider>())
            {
                GetComponent<BoxCollider>().enabled = false;
            }else if (GetComponent<SphereCollider>())
            {
                GetComponent<SphereCollider>().enabled = false;
            }

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
        
        if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().enabled = true;

        }else if (GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().enabled = true;
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

