using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMov : MonoBehaviour
{
    public float velocidadPowerUp;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(0f, 0f, velocidadPowerUp * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }


}
