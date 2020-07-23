using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{

    public Transform pickObjectDestination;
    public float pickDistance = 3f;


    private void OnMouseDown()
    {
        if(Vector3.Distance(transform.position, pickObjectDestination.root.position) < pickDistance)//YOu cannot pick up the object if you are too far away
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = pickObjectDestination.position;
            this.transform.parent = GameObject.Find("PickObject").transform;
        }     
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}

