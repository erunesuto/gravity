using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public string beaconColor;
    public GameObject movingBlock;
    [Tooltip("Position where the beacon is not activated")]
    public Transform originalPosition;
    [Tooltip("Position to move the block. Position where the beacon is activated")]
    public Transform newPosition;
    [Tooltip("The speed the block moves")]
    public float speed;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (canMove == true)
        {
            float fixedSpeed = speed * Time.deltaTime;
            movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, newPosition.position, fixedSpeed);
        }
        else
        {
            float fixedSpeed = speed * Time.deltaTime;
            movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, originalPosition.position, fixedSpeed);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Key>().keyColor == beaconColor)
        {
            canMove = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            canMove = false;
        }
    }


}
