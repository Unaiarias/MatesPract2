using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 4f;
    public float timelife = 5f;





    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Destroy(gameObject);

        }
    }
}
