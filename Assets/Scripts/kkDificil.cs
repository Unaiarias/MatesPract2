using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class kkDificil : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private float timeAux;
    public GameObject bala;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("JugadorDificil");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        transform.LookAt(target.transform);


        if (transform.position.z <= target.transform.position.z)
        {
            if (Time.time - timeAux >= 2.0f)
            {
                Instantiate(bala, transform.position, transform.rotation);
                timeAux = Time.time;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
