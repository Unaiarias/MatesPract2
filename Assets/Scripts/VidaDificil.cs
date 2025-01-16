using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDificil : MonoBehaviour
{
    public float velocidadcubo;
    [SerializeField]
    float rotationSpeedX, rotationSpeedY, rotationSpeedZ;


    public float healingAmount;

    public GameObject playerObj;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.Translate(0f, 0f, velocidadcubo * Time.deltaTime);
        //transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            if (playerObj.GetComponent<JugadorMovDificil>().vidaActual + healingAmount > 100) playerObj.GetComponent<JugadorMovDificil>().vidaActual = 100;
            else playerObj.GetComponent<JugadorMovDificil>().vidaActual += healingAmount;


            Destroy(gameObject);

        }
    }
}
