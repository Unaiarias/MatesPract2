using System.IO.Ports;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    public string puertoCOM = "COM7";
    SerialPort puerto;

    public int x;
    public int y;
    public bool boton;

    int ultimaVida = -1;

    // --- DEBOUNCE ---
    float cooldownBoton = 0.2f; // 200 ms
    float tiempoUltimoPulsado = 0f;

    void Start()
    {
        puerto = new SerialPort(puertoCOM, 9600);
        puerto.ReadTimeout = 20;
        puerto.Open();
    }

    void Update()
    {
        try
        {
            string linea = puerto.ReadLine();

            if (linea.Contains(","))
            {
                string[] partes = linea.Split(',');
                x = int.Parse(partes[0]);
                y = int.Parse(partes[1]);
            }
            else if (linea == "BOTON_PULSADO")
            {
                // --- DEBOUNCE ---
                if (Time.time - tiempoUltimoPulsado > cooldownBoton)
                {
                    boton = true;
                    tiempoUltimoPulsado = Time.time;
                }
            }
        }
        catch { }
    }

    public void Enviar(string msg)
    {
        int valor = int.Parse(msg);

        if (puerto.IsOpen && valor != ultimaVida)
        {
            puerto.WriteLine(msg);
            ultimaVida = valor;
        }
    }

    public void ResetBoton()
    {
        boton = false;
    }
}
