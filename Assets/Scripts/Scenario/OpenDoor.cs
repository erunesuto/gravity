using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    
    public GameObject door;
    [Tooltip("Position to move the door")]
    public string beaconColor;
    public Transform newPosition;
    private bool openDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
 void Update()
    {
        //Debug.Log(beaconColor);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        openDoor = false;
        
    }


    private void test()
    {
     
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            openDoor = true;
            Debug.Log("se abrio el sesamo");
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Key")
        {
            if (other.GetComponentInParent<Key>().keyColor == beaconColor)
            {
                Debug.Log("el color coincide, open");
            }
            openDoor = true;
            Debug.Log(other.GetComponentInParent<Key>().keyColor +" y " + this.beaconColor +" ? ");
        }*/

        if (other.GetComponentInParent<Key>().keyColor == beaconColor)
        {
            Debug.Log("el color coincide, open");
        }
        aki, no detecta el beaconcolor
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            
            openDoor = false;
            Debug.Log("se serrço de serar el sesamo");
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Key"))
        {
            Debug.Log("collision");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Key"))
        {
            Debug.Log("sliendo colision");
        }
    }*/

}
