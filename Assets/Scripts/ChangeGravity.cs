using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{

    //public FPSController fpsController;
    public float gravity;
    void Start()
    {
        gravity = GetComponent<FPSController>().gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))//cambiar el key por button
        {
            Debug.Log("Pulsado G");
            gravity = -gravity;
        }
    }
}
