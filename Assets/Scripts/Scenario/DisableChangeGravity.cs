﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChangeGravity : MonoBehaviour  
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FPSController>().changeGravityAvailable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FPSController>().changeGravityAvailable = true;
        }
    }
}