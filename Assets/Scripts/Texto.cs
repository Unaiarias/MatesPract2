using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Texto : MonoBehaviour
{

    public TMP_Text textoTiempo;
    public TMP_Text textoVidas;

    

    void Start()
    {
        
    }

   
    void Update()
    {
        textoVidas.text = "Vidas: ";
        textoTiempo.text = "Contador: " + Time.time.ToString("F2");


         
    }
}
