using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 4f;
    public float timelife = 5f;
    public float damageAmount;

    

    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {

        playerObj = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            playerObj.GetComponent<JugadorMov>().vidaActual -= damageAmount;


            Destroy(gameObject);

        }
    }
}
