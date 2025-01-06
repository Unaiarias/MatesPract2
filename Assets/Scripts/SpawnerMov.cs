using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMov : MonoBehaviour
{
    public float velocidadSpawner;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(0f, 0f, velocidadSpawner * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }


}
