using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public string beaconColor;
    public GameObject movingBlock;
    [Tooltip("Position when the beacon is not activated")]
    public Transform originalPosition;
    [Tooltip("Position to move the block. Position when the beacon is activated")]
    public Transform newPosition;
    [Tooltip("The speed the block moves. 0 for not moving")]
    public float speed;
    public float rotationSpeed;
    public float resizeSpeed;

    //[Tooltip("the time it takes for the block to rotate. 0 for no rotation")]
    //public float time;
    [Tooltip("Activate the resize")]
    public bool resize = false;
    private bool canMove = false;

    [Tooltip("The original position scale have to be the same than de movingBlock for a correct performance")]
    public bool explanation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        if (canMove == true)
        {
            float fixedSpeed = speed * Time.deltaTime;
            movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, newPosition.position, speed * Time.deltaTime);
            movingBlock.transform.localEulerAngles = Vector3.RotateTowards(movingBlock.transform.localEulerAngles, newPosition.localEulerAngles, 0, rotationSpeed * Time.deltaTime);//dont know what the "0" does.It seems works
            if (resize)
            {
                movingBlock.transform.localScale = Vector3.Lerp(movingBlock.transform.localScale, newPosition.localScale, resizeSpeed * Time.deltaTime);
            }
        }
        else
        {
            float fixedSpeed = speed * Time.deltaTime;
            movingBlock.transform.position = Vector3.MoveTowards(movingBlock.transform.position, originalPosition.position, speed * Time.deltaTime);
            movingBlock.transform.localEulerAngles = Vector3.RotateTowards(movingBlock.transform.localEulerAngles, originalPosition.localEulerAngles, 0, rotationSpeed * Time.deltaTime);
            if (resize)
            {
                movingBlock.transform.localScale = Vector3.Lerp(movingBlock.transform.localScale, originalPosition.localScale, resizeSpeed * Time.deltaTime);
            }
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



    #region COROUTINES
    IEnumerator RotateFixedDegrees(Vector3 finalRotation, float inTime)//rotate to Xrotation in X seconds.
    {

        var fromAngle = movingBlock.transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            movingBlock.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        
    }

    IEnumerator ChangeScale(Vector3 finalScale, float inTime)//rescalate to localscale.
    {

        var fromAngle = movingBlock.transform.localScale;
        var toAngle = finalScale;
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            movingBlock.transform.localScale = Vector3.Lerp(fromAngle, toAngle, t);
            yield return null;
        }

    }
    #endregion

}
