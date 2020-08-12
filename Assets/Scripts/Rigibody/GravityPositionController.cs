using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPositionController : MonoBehaviour
{
    public Transform detector1;
    public Transform detector2;
    public Transform rotationMecanism;
    public Transform movingDetector;
    public GameObject player;

    private float distanceMovingDetectorToDetector1;
    private float distanceMovingDetectorToDetector2;
    public static bool invertChangeGravityControlls = false;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player"); //find the player
    }

    // Update is called once per frame
    void Update()
    {

        rotationMecanism.rotation = player.transform.rotation;

        distanceMovingDetectorToDetector1 = Vector3.Distance(detector1.position, movingDetector.position);
        distanceMovingDetectorToDetector2 = Vector3.Distance(detector2.position, movingDetector.position);

        if(distanceMovingDetectorToDetector1 <= distanceMovingDetectorToDetector2)
        {
            invertChangeGravityControlls = false;
        }
        else
        {
            invertChangeGravityControlls = true;
        }

    }
}
