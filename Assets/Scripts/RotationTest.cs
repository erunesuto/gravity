using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    float rotateSpeed = 90;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            //Quaternion rotation = Quaternion.AngleAxis(5, Vector3.forward);
            float angle = rotateSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
           
            Debug.Log("entra");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //Quaternion rotation = Quaternion.AngleAxis(5, Vector3.forward);
            float angle = rotateSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);

            Debug.Log("entra rutina");
        }
    }


    IEnumerator RotateFixedDegrees(Vector3 finalRotation, float inTime)//rotate to Xposition in X seconds. Used
    {

        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(finalRotation);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            //transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.back);
            yield return null;
        }

    }
}
