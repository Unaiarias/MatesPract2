using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Texto : MonoBehaviour
{

    public TMP_Text textoTiempo;
    public TMP_Text textoVidas;
    int vidas;

    void Start()
    {
        vidas = 5;
    }

   
    void Update()
    {
        textoVidas.text = "Vidas: " + vidas;
        textoTiempo.text = "Contador: " + Time.time.ToString("F2");
    }
}
