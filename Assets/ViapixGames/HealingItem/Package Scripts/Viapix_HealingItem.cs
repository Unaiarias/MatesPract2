using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viapix_PlayerParams;

namespace Viapix_HealingItem
{

    public class Viapix_HealingItem : MonoBehaviour
    {
        public float velocidadcubo;
        [SerializeField]
        float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

        [SerializeField]
        int healingAmount;

        GameObject playerObj;

        private void Start()
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
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerObj.GetComponent<Viapix_PlayerHP>().playerHP += healingAmount;

                Destroy(gameObject);

                print("Player HP: " + playerObj.GetComponent<Viapix_PlayerHP>().playerHP);
            }
        }
    }
}

