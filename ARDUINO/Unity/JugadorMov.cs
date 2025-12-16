using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JugadorMov : MonoBehaviour
{
    public ArduinoInput arduino;

    public float velocidad = 5.0f;
    public float salto = 8.0f;
    public float gravedad = 9.8f;

    private Vector3 direccion = Vector3.zero;

    public Image barraDeVida;

    public float vidaActual;
    public float vidaMaxima = 100f;

    void Start()
    {
        vidaActual = 100f;
    }

    void Update()
    {
        // Evita valores negativos
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        barraDeVida.fillAmount = vidaActual / vidaMaxima;

        float porcentaje = vidaActual / vidaMaxima;

        int vidas = 0;
        if (porcentaje > 0.66f) vidas = 3;
        else if (porcentaje > 0.33f) vidas = 2;
        else if (porcentaje > 0f) vidas = 1;
        else vidas = 0;
        Debug.Log("VIDAS REALES = " + vidas);
        // Enviar vidas al Arduino
        arduino.Enviar(vidas.ToString());

        if (barraDeVida.fillAmount == 0.0f)
        {
            SceneManager.LoadScene("MiniJuegoFacil");
        }

        CharacterController controller = GetComponent<CharacterController>();

        float horizontal = Mathf.Lerp(-1f, 1f, arduino.x / 15f);
        float vertical = Mathf.Lerp(-1f, 1f, arduino.y / 2f);

        if (controller.isGrounded)
        {
            direccion = new Vector3(-horizontal, 0, -vertical);
            direccion = transform.TransformDirection(direccion);
            direccion *= velocidad;

            // --- SALTAR CON BOTÓN DEL ARDUINO ---
            if (arduino.boton)
            {
                direccion.y = salto;
                arduino.ResetBoton();  // ← Importante para evitar saltos múltiples
            }
        }

        direccion.y -= gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * Time.deltaTime;
        controller.Move(movimiento);

        // Límites de Z
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
