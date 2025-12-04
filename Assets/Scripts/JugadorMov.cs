using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class JugadorMov : MonoBehaviour
{
    public ArduinoInput arduino;   // ← AÑADIDO

    public float velocidad = 5.0f;
    public float salto = 8.0f;
    public float gravedad = 9.8f;
    private Vector3 direccion = Vector3.zero;

    public Image barraDeVida;

    public float vidaActual;
    public float vidaMaxima;

    void Start()
    {
        vidaActual = 100f;
    }

    void Update()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;

        // ---- CONVERTIR VIDA (0–100) A 3–0 ----
        float porcentaje = vidaActual / vidaMaxima;

        int vidas = 0;
        if (porcentaje > 0.66f) vidas = 3;
        else if (porcentaje > 0.33f) vidas = 2;
        else if (porcentaje > 0f) vidas = 1;
        else vidas = 0;

        // ENVIAR AL ARDUINO
        arduino.Enviar(vidas.ToString());

        if (barraDeVida.fillAmount == 0.0f)
        {
            SceneManager.LoadScene("MiniJuegoFacil");
        }

        CharacterController controller = GetComponent<CharacterController>();

        // ---- LEER POTENCIOMETROS ----
        float horizontal = Mathf.Lerp(-1f, 1f, arduino.x / 15f);
        float vertical = Mathf.Lerp(-1f, 1f, arduino.y / 2f);

        if (controller.isGrounded)
        {
            direccion = new Vector3(-horizontal, 0, -vertical);
            direccion = transform.TransformDirection(direccion);
            direccion *= velocidad;

            if (arduino.boton)
                direccion.y = salto;
        }

        direccion.y -= gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * Time.deltaTime;
        controller.Move(movimiento);

        // ---- LIMITES EN Z ----
        if (vertical < 0 && transform.position.z > 82.0f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 82.0f);

        if (vertical > 0 && transform.position.z < 45.0f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 45.0f);
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
