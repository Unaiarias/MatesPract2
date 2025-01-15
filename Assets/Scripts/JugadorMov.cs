using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorMov : MonoBehaviour
{

    public float velocidad = 5.0f;
    public float salto = 8.0f;
    public float gravedad = 9.8f;
    private Vector3 direccion = Vector3.zero;

    public Image barraDeVida;

    public float vidaActual;

    public float vidaMaxima;

    //public Viapix_HealingItem health;
    void Start()
    {

    }


    void Update()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;

        if (barraDeVida.fillAmount == 0.0f)
        {
            SceneManager.LoadScene("MiniJuegoFacil");
        }

        
        

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            direccion = new Vector3(-Input.GetAxis("Horizontal"), 0,
           -Input.GetAxis("Vertical"));
            direccion = transform.TransformDirection(direccion);
            direccion *= velocidad;
            if (Input.GetButton("Jump"))
                direccion.y = salto;
        }
        direccion.y -= gravedad * Time.deltaTime;
        Vector3 movimiento = direccion * Time.deltaTime;      

        //float clampZ = Mathf.Clamp(movimiento.z, 45.0f, 500.0f);

        controller.Move(movimiento);
        
        if (Input.GetAxis("Vertical") < 0) 
            if (transform.position.z > 82.0f)
                transform.position = new Vector3(transform.position.x, transform.position.y, 82.0f);

        if (Input.GetAxis("Vertical") > 0)
            if (transform.position.z < 45.0f)
                transform.position = new Vector3(transform.position.x, transform.position.y, 45.0f);
        //transform.position = new Vector3(transform.position.x, transform.position.y, clampZ);


        /*if (Input.GetAxis("Vertical") < 0)
        {
             posZ = transform.position.z + movZ * velocidad * Time.deltaTime;
             clampZ = Mathf.Clamp(posZ, posZ, 82.0f);

            transform.position = new Vector3(transform.position.x, transform.position.y, clampZ);
        }*/


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            SceneManager.LoadScene("MiniJuegoFacil");
        }

    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Colisión con un objeto de tipo: " + hit.gameObject.tag);

    }
}
