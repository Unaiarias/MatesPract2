using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboMovDificil : MonoBehaviour
{
    public float velocidadcubo;
    void Start()
    {

    }


    void Update()
    {
        transform.Translate(0f, 0f, velocidadcubo * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
