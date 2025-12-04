using System.IO.Ports;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    public string puertoCOM = "COM3"; // CAMBIAR A TU PUERTO REAL
    SerialPort puerto;

    public int x;
    public int y;
    public bool boton;

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
                boton = true;
            }
        }
        catch { }

        // Reset del botón cada frame
        boton = false;
    }

    public void Enviar(string msg)
    {
        if (puerto.IsOpen)
        {
            puerto.WriteLine(msg);
        }
    }
}
