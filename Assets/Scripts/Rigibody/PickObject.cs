using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{

    
    private GameObject pickObjectDestination;
    public static float pickDistance = 3f;
    public bool thisItemUseGravity = true;

    private void Start()
    {
        pickObjectDestination = GameObject.Find("/PlayerRB/PickObject");
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

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            //this.transform.position = pickObjectDestination.position;
            this.transform.position = pickObjectDestination.transform.position;
            this.transform.parent = GameObject.Find("PickObject").transform;      

        }     
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        if (thisItemUseGravity)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        //GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}

