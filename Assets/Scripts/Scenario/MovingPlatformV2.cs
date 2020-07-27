using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformV2 : MonoBehaviour
{
    GameObject player;
    private Transform target;
    public float speed;
    int actualTarget = 0;
    //[Range(2, 5)]
    public Transform[] targetsArray = new Transform[2];
    

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //find the player
        target = targetsArray[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Se encarga de mover la plataforma
        if (target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);

        }

        //reinicia el ciclo de targets cuando llega al ultimo
        if (actualTarget == targetsArray.Length)
        {
            actualTarget = 0;
        }

        //va cambiando de target. Cambia al target siguiente del array al llegar a la posicion del actual
        if (actualTarget < targetsArray.Length)
        {
            target = targetsArray[actualTarget];
        }

        //cuando llega a la posicion del target para al siguiente target
        //si la posicion de la plataforma == posicion objetivo (ha llegao a su destino)
        if (transform.position == target.position)
        {
            actualTarget++;  
        }

    }

    //Controla que el personaje se mueva con la plataforma al entrar en contacto con la misma
    //Hace al jugador hijo de la plataforma para conseguir que se muevan juntos
   
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }


    //Hace que el jugador no sea hijo de la plataforma
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}